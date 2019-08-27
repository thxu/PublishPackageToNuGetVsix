using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PublishPackageToNuGet2017.Model;

namespace PublishPackageToNuGet2017.NuGetHelper
{
    public class NuGetPkgAnalysisFromNewCsproj : INuGetPkgAnalysis
    {
        public Dictionary<string, List<SimplePkgView>> GetNuGetPkgs(ProjModel _projModel)
        {
            Dictionary<string, List<SimplePkgView>> res = new Dictionary<string, List<SimplePkgView>>();
            try
            {
                var configFilePath = _projModel.ProjectFullName;
                if (File.Exists(configFilePath))
                {
                    XElement pkgsElement = XDocument.Load(configFilePath).Element("Project");
                    if (pkgsElement == null)
                    {
                        return res;
                    }
                    var allItemGroups = pkgsElement.Elements("ItemGroup").ToList();
                    if (allItemGroups == null || !allItemGroups.Any())
                    {
                        return res;
                    }

                    foreach (XElement itemGroup in allItemGroups)
                    {
                        var pkgList = itemGroup.Elements("PackageReference").ToList();
                        if (pkgList == null || !pkgList.Any())
                        {
                            continue;
                        }

                        var targets = new List<string>();
                        var condition = itemGroup.Attribute("Condition")?.Value ?? string.Empty;
                        if (string.IsNullOrWhiteSpace(condition))
                        {
                            targets = _projModel.NetFrameworkVersionList;
                        }
                        else
                        {
                            targets = GetTargetFormCondition(condition, _projModel);
                        }

                        foreach (XElement pkgElement in itemGroup.Elements("PackageReference"))
                        {
                            if (targets != null && targets.Any())
                            {
                                foreach (string targetFramework in targets)
                                {
                                    var id = pkgElement.Attribute("Include")?.Value ?? string.Empty;
                                    var version = pkgElement.Attribute("Version")?.Value ?? string.Empty;
                                    //var targetFramework = pkgElement.Attribute("targetFramework")?.Value ?? string.Empty;
                                    if (!string.IsNullOrWhiteSpace(id) && !string.IsNullOrWhiteSpace(version))
                                    {
                                        SimplePkgView model = new SimplePkgView
                                        {
                                            Id = id,
                                            Version = version,
                                            TargetFramework = targetFramework,
                                        };
                                        if (res.ContainsKey(targetFramework))
                                        {
                                            res[targetFramework].Add(model);
                                        }
                                        else
                                        {
                                            res.Add(targetFramework, new List<SimplePkgView>() { model });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
            return res;
        }

        private List<string> GetTargetFormCondition(string condition, ProjModel _projModel)
        {
            List<string> res = new List<string>();

            // 格式：Condition="'$(TargetFramework)' == 'netstandard2.0' Or '$(TargetFramework)' == 'net45'"
            if (condition.Contains("$(TargetFramework)"))
            {
                List<string> ops = new List<string>();
                if (condition.Contains("And"))
                {
                    ops = condition.Split('A', 'n', 'd').ToList();
                }
                else if (condition.Contains("Or"))
                {
                    ops = condition.Split('O', 'r').ToList();
                }
                else
                {
                    ops.Add(condition);
                }

                foreach (string op in ops)
                {
                    if (op.Contains("$(TargetFramework)") && op.Contains("=="))
                    {
                        var tmp = op.Split('=', '=');

                        var targetCondition = tmp[0].Contains("$(TargetFramework)") ? tmp[2] : tmp[0];
                        var target = _projModel.NetFrameworkVersionList.FirstOrDefault(n => targetCondition.Contains(n));
                        if (target != null)
                        {
                            res.Add(target);
                        }
                    }
                }
            }

            return res;
        }
    }
}
