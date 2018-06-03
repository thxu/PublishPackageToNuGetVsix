using System;
using System.IO;
using System.Text.RegularExpressions;
using EnvDTE;
using PublishPackageToNuGet.Model;

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
    }
}
