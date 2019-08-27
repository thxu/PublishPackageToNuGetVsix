using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PublishPackageToNuGet2017.Model;

namespace PublishPackageToNuGet2017.NuGetHelper
{
    public class NuGetPkgAnalysisFromOldCsproj: INuGetPkgAnalysis
    {
        public Dictionary<string, List<SimplePkgView>> GetNuGetPkgs(ProjModel _projModel)
        {
            Dictionary<string, List<SimplePkgView>> res = new Dictionary<string, List<SimplePkgView>>();

            try
            {
                var configFilePath = Path.Combine(_projModel.ProjectPath, "packages.config");
                if (File.Exists(configFilePath))
                {
                    XElement pkgsElement = XDocument.Load(configFilePath).Element("packages");
                    if (pkgsElement == null)
                    {
                        return res;
                    }
                    var allPkgs = pkgsElement.Elements("package");
                    if (allPkgs == null || !allPkgs.Any())
                    {
                        return res;
                    }
                    foreach (XElement pkgElement in allPkgs)
                    {
                        var id = pkgElement.Attribute("id")?.Value ?? string.Empty;
                        var version = pkgElement.Attribute("version")?.Value ?? string.Empty;
                        var targetFramework = pkgElement.Attribute("targetFramework")?.Value ?? string.Empty;
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
            catch (Exception e)
            {
                
            }

            return res;
        }
    }
}
