using EnvDTE;
using Microsoft.VisualStudio.Shell;
using NuGet.Packaging;
using PublishPackageToNuGet2017.Model;
using System;
using System.Collections.Generic;
using System.IO;

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
                NetFrameworkVersion = project.Properties.Item("TargetFrameworkMoniker").Value.ToString(),
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
    }
}
