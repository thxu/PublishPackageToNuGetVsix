using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using NuGet.Common;
using NuGet.Frameworks;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using NuGet.VisualStudio;
using PublishPackageToNuGet2017.Command;
using PublishPackageToNuGet2017.Model;
using PublishPackageToNuGet2017.Setting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using IAsyncServiceProvider = Microsoft.VisualStudio.Shell.IAsyncServiceProvider;

namespace PublishPackageToNuGet2017.Service
{
    public static class NuGetPkgPublishService
    {
        public static ProjModel AnalysisProject(this Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            string projName = project.FullName;
            ProjModel model = new ProjModel
            {
                Key = Guid.NewGuid().ToString(),
                LibName = Path.GetFileNameWithoutExtension(projName),
                LibDebugPath = project.GetFullOutputPath(),
                LibReleasePath = string.Empty,
                Author = string.Empty,
                Desc = string.Empty,
                NetFrameworkVersion = NuGetFramework.Parse(project.Properties.Item("TargetFrameworkMoniker").Value.ToString()).GetShortFolderName(),
                Owners = new List<string>(),
                PackageInfo = new ManifestMetadata(),
                Version = string.Empty,
            };

            var outputType = project.Properties.Item("OutputType").Value.ToString();

            if (outputType == "2")
            {
                return model;
            }

            return null;
        }

        /// <summary>
        /// 获取当前选中项目的输出目录
        /// </summary>
        /// <param name="project">项目信息</param>
        /// <returns>输出路径</returns>
        private static string GetFullOutputPath(this Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var outputPath = (string)project.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value;
            var projFullPath = project.Properties.Item("FullPath").Value?.ToString();
            if (string.IsNullOrWhiteSpace(projFullPath))
            {
                return string.Empty;
            }
            DirectoryInfo projDirectoryInfo = new DirectoryInfo(projFullPath);

            while (outputPath.StartsWith(@"..\"))
            {
                projDirectoryInfo = projDirectoryInfo?.Parent;
                outputPath = outputPath.Substring(3);
            }

            outputPath = outputPath.TrimStart('.', '\\');

            var path = Path.Combine(projDirectoryInfo?.FullName ?? string.Empty, outputPath);
            return path.TrimEnd('\\');
        }

        public static NuGetPkgPublishPackage GetSettingPackage()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            if (Package.GetGlobalService(typeof(SVsShell)) is IVsShell shell)
            {
                Guid guid = new Guid(NuGetPkgPublishPackage.PackageGuidString);
                if (ErrorHandler.Succeeded(shell.IsPackageLoaded(ref guid, out var package)))
                {
                    return package as NuGetPkgPublishPackage;
                }

                if (ErrorHandler.Succeeded(shell.LoadPackage(ref guid, out package)))
                {
                    return package as NuGetPkgPublishPackage;
                }
            }
            return null;
        }

        public static OptionPageGrid GetSettingPage()
        {
            NuGetPkgPublishPackage package = GetSettingPackage();
            return package?.GetDialogPage(typeof(OptionPageGrid)) as OptionPageGrid;
        }

        public static List<string> GetAllPackageSources()
        {
            IAsyncServiceProvider provider = GetSettingPackage();

            var tmp = provider.GetServiceAsync(typeof(SComponentModel)).Result;

            var componentModel = (IComponentModel)tmp;
            if (componentModel == null) throw new ArgumentNullException(nameof(componentModel));
            var sourcePorvider = componentModel.GetService<IVsPackageSourceProvider>();
            var sc = sourcePorvider.GetSources(true, false);
            return sc.Select(n => n.Value).ToList();
        }

        public static ManifestMetadata GetPackageData(this string packageFullName, string packageSourceUrl)
        {
            var repository = PackageRepositoryFactory.CreateRepository(packageSourceUrl);
            try
            {
                IEnumerable<IPackageSearchMetadata> simpleDataList = null;
                var rawPackageSearchResouce = ThreadHelper.JoinableTaskFactory.Run((() => repository.GetResourceAsync<RawSearchResourceV3>(CancellationToken.None)));
                if (rawPackageSearchResouce != null)
                {
                    var json = ThreadHelper.JoinableTaskFactory.Run(() => rawPackageSearchResouce.Search(packageFullName, new SearchFilter(true), 0, 10, NullLogger.Instance, CancellationToken.None));
                    simpleDataList = json.Select(s => s.FromJToken<PackageSearchMetadata>());
                }

                if (simpleDataList == null || !simpleDataList.Any())
                {
                    var packageSearchResouce = ThreadHelper.JoinableTaskFactory.Run(() => repository.GetResourceAsync<PackageSearchResource>(CancellationToken.None));
                    simpleDataList = ThreadHelper.JoinableTaskFactory.Run(() => packageSearchResouce.SearchAsync(packageFullName, new SearchFilter(true), 0, 10, NullLogger.Instance, CancellationToken.None));
                }

                var simpleData = simpleDataList.FirstOrDefault(n => n.Identity.Id == packageFullName);
                if (simpleData == null)
                {
                    return null;
                }

                var downloadResource = ThreadHelper.JoinableTaskFactory.Run(() => repository.GetResourceAsync<DownloadResource>());

                using (var sourceCacheContext = new SourceCacheContext() { NoCache = true })
                {
                    var context = new PackageDownloadContext(sourceCacheContext, Path.GetTempPath(), true);

                    using (var result = ThreadHelper.JoinableTaskFactory.Run(() => downloadResource.GetDownloadResourceResultAsync(simpleData.Identity, context, string.Empty, NullLogger.Instance, CancellationToken.None)))
                    {
                        if (result.Status == DownloadResourceResultStatus.Cancelled)
                        {
                            throw new OperationCanceledException();
                        }
                        if (result.Status == DownloadResourceResultStatus.NotFound)
                        {
                            throw new Exception($"Package '{simpleData.Identity.Id} {simpleData.Identity.Version}' not found");
                        }

                        using (var reader = new PackageArchiveReader(result.PackageStream))
                        {
                            var manifest = Manifest.ReadFrom(reader.GetNuspec(), false);
                            return manifest.Metadata;
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            return null;
        }

        public static List<SimplePkgView> GetPkgList(this string packageSourceUrl, string pkgName, int skip, int take = 10)
        {
            try
            {
                if (skip <= 0)
                {
                    skip = 0;
                }
                var repository = PackageRepositoryFactory.CreateRepository(packageSourceUrl);
                IEnumerable<IPackageSearchMetadata> simpleDataList = null;
                var rawPackageSearchResouce = ThreadHelper.JoinableTaskFactory.Run((() => repository.GetResourceAsync<RawSearchResourceV3>(CancellationToken.None)));
                if (rawPackageSearchResouce != null)
                {
                    var json = ThreadHelper.JoinableTaskFactory.Run(() => rawPackageSearchResouce.Search(pkgName, new SearchFilter(true), skip, take, NullLogger.Instance, CancellationToken.None));
                    simpleDataList = json.Select(s => s.FromJToken<PackageSearchMetadata>());
                }

                if (simpleDataList == null || !simpleDataList.Any())
                {
                    var packageSearchResouce = ThreadHelper.JoinableTaskFactory.Run(() => repository.GetResourceAsync<PackageSearchResource>(CancellationToken.None));
                    simpleDataList = ThreadHelper.JoinableTaskFactory.Run(() => packageSearchResouce.SearchAsync(pkgName, new SearchFilter(true), skip, take, NullLogger.Instance, CancellationToken.None));
                }

                if (simpleDataList != null && simpleDataList.Any())
                {
                    var res = simpleDataList.Select(n => new SimplePkgView
                    {
                        Desc = n.Description,
                        Id = n.Identity.Id,
                        Version = n.Identity.Version.OriginalVersion,
                        Author = n.Authors,
                    }).ToList();
                    return res;
                }
            }
            catch (Exception e)
            {

            }
            return null;
        }

        public static string AddVersion(this string version)
        {
            if (string.IsNullOrWhiteSpace(version))
            {
                return "1.0.0.1";
            }
            string[] tmp = version.Split('.');
            int val = Convert.ToInt32(tmp[tmp.Length - 1]) + 1;
            tmp[tmp.Length - 1] = val.ToString();
            return string.Join(".", tmp);
        }

        public static string BuildPackage(this ProjModel pkg)
        {
            string dllPath = Path.Combine(pkg.LibDebugPath, pkg.LibName + ".dll");
            string xmlPath = Path.Combine(pkg.LibDebugPath, pkg.LibName + ".xml");

            if (!File.Exists(dllPath))
            {
                throw new Exception("未找到DLL文件, 请先编译项目");
            }
            MyPackageFile dllFile = new MyPackageFile("lib", pkg.LibName + ".dll", dllPath, pkg.NetFrameworkVersion);
            List<IPackageFile> files = new List<IPackageFile>() { dllFile };
            if (File.Exists(xmlPath))
            {
                MyPackageFile xmlFile = new MyPackageFile("lib", pkg.LibName + ".xml", xmlPath, pkg.NetFrameworkVersion);
                files.Add(xmlFile);
            }

            ManifestMetadata data = pkg.PackageInfo ?? new ManifestMetadata
            {
                Authors = new List<string> { pkg.Author },
                ContentFiles = new List<ManifestContentFiles>(),
                Copyright = $"CopyRight © {pkg.Author} {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
                DependencyGroups = new List<PackageDependencyGroup>(),
                Description = pkg.Desc,
                DevelopmentDependency = false,
                FrameworkReferences = new List<FrameworkAssemblyReference>(),
                Id = pkg.LibName,
                Language = null,
                MinClientVersionString = "1.0.0.0",
                Owners = new List<string> { pkg.Author },
                PackageAssemblyReferences = new List<PackageReferenceSet>(),
                PackageTypes = new List<PackageType>(),
                ReleaseNotes = null,
                Repository = null,
                RequireLicenseAcceptance = false,
                Serviceable = false,
                Summary = null,
                Tags = string.Empty,
                Title = pkg.LibName,
                Version = NuGetVersion.Parse(pkg.Version),
            };

            data.Authors = new List<string> { pkg.Author };
            data.Copyright = $"CopyRight © {pkg.Author} {DateTime.Now:yyyy-MM-dd HH:mm:ss}";
            data.Description = pkg.Desc;
            data.Version = NuGetVersion.Parse(pkg.Version);

            var builder = new PackageBuilder();
            builder.Populate(data);
            builder.Files.AddRange(files);

            var packageFile = Path.GetTempFileName();
            try
            {
                using (Stream stream = File.Create(packageFile))
                {
                    builder.Save(stream);
                }
            }
            finally
            {
            }

            return packageFile;
        }

        public static bool PushToNugetSer(this string filePath, string publishKey, string publishUrl)
        {
            var repository = PackageRepositoryFactory.CreateRepository(publishUrl);
            var updateResource = ThreadHelper.JoinableTaskFactory.Run(() => repository.GetResourceAsync<PackageUpdateResource>());
            try
            {
                ThreadHelper.JoinableTaskFactory.Run(() => updateResource.Push(filePath, null, 999, false, s => publishKey, s => publishKey, true, NullLogger.Instance));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
