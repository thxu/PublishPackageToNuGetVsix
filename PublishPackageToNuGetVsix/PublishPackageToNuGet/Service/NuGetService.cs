using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using NuGet.Common;
using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;
using NuGet.VisualStudio;
using PublishPackageToNuGet.Model;
using PublishPackageToNuGet.Setting;

namespace PublishPackageToNuGet.Service
{
    public static class NuGetService
    {
        public static ProjModel AnalysisProject(this Project project)
        {
            string projName = project.FullName;
            ProjModel model = new ProjModel
            {
                Key = Guid.NewGuid().ToString(),
                LibName = Path.GetFileNameWithoutExtension(projName),
            };
            string txt = File.ReadAllText(projName);

            Regex regex = new Regex(@"<PropertyGroup(\s|\S)*?>(\s|\S)*?</PropertyGroup>");
            var regexRes = regex.Matches(txt);
            bool canAdd = false;
            foreach (Match match in regexRes)
            {
                if (match.Value.Contains("Debug|AnyCPU"))
                {
                    Regex reg = new Regex(@"<OutputPath>(?<Word>(\s|\S)*?)</OutputPath>");
                    var typeMatch = reg.Match(match.Value);
                    if (typeMatch.Success)
                    {
                        model.LibDebugPath = typeMatch.Value.Contains(":")
                            ? typeMatch.Groups["Word"].Value
                            : Path.Combine(Path.GetDirectoryName(projName) ?? string.Empty, typeMatch.Groups["Word"].Value);
                    }
                }
                if (match.Value.Contains("Release|AnyCPU"))
                {
                    Regex reg = new Regex(@"<OutputPath>(?<Word>(\s|\S)*?)</OutputPath>");
                    var typeMatch = reg.Match(match.Value);
                    if (typeMatch.Success)
                    {
                        model.LibReleasePath = typeMatch.Value.Contains(":")
                            ? typeMatch.Groups["Word"].Value
                            : Path.Combine(Path.GetDirectoryName(projName) ?? string.Empty, typeMatch.Groups["Word"].Value);
                    }
                }
                if (match.Value.Contains("OutputType"))
                {
                    Regex reg = new Regex(@"<OutputType>(?<Word>(\s|\S)*?)</OutputType>");
                    var typeMatch = reg.Match(match.Value);
                    if (typeMatch.Success && typeMatch.Groups["Word"].Value == "Library")
                    {
                        canAdd = true;
                    }

                    reg = new Regex(@"<TargetFrameworkVersion>(?<Word>(\s|\S)*?)</TargetFrameworkVersion>");
                    typeMatch = reg.Match(match.Value);
                    if (typeMatch.Success)
                    {
                        model.NetFrameworkVersion = typeMatch.Groups["Word"].Value.Replace("v", "net").Replace(".", "");
                    }
                }
            }

            if (canAdd)
            {
                return model;
            }

            return null;
        }

        public static PublishCommandPackage GetSettingPackage()
        {
            if (Package.GetGlobalService(typeof(SVsShell)) is IVsShell shell)
            {
                Guid guid = new Guid(PublishCommandPackage.PackageGuidString);
                if (ErrorHandler.Succeeded(shell.IsPackageLoaded(ref guid, out var package)))
                {
                    return package as PublishCommandPackage;
                }

                if (ErrorHandler.Succeeded(shell.LoadPackage(ref guid, out package)))
                {
                    return package as PublishCommandPackage;
                }
            }
            return null;
        }

        public static OptionPageGrid GetSettingPage()
        {
            PublishCommandPackage package = GetSettingPackage();
            return package?.GetDialogPage(typeof(OptionPageGrid)) as OptionPageGrid;
        }

        public static List<string> GetAllPackageSources()
        {
            IServiceProvider provider = GetSettingPackage();

            var componentModel = (IComponentModel)provider.GetService(typeof(SComponentModel));
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
                throw new Exception("未找到DLL文件");
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

        public static void PushToNugetSer(this string filePath, string publishKey, string publishUrl)
        {
            var repository = PackageRepositoryFactory.CreateRepository(publishUrl);
            var updateResource = ThreadHelper.JoinableTaskFactory.Run(() => repository.GetResourceAsync<PackageUpdateResource>());
            ThreadHelper.JoinableTaskFactory.Run(() => updateResource.Push(filePath, null, 999, false, s => publishKey, s => publishKey, true, NullLogger.Instance));
        }
    }
}
