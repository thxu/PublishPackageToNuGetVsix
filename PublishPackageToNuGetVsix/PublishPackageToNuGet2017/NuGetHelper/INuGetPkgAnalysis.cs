using System.Collections.Generic;
using PublishPackageToNuGet2017.Model;

namespace PublishPackageToNuGet2017.NuGetHelper
{
    public interface INuGetPkgAnalysis
    {
        Dictionary<string, List<SimplePkgView>> GetNuGetPkgs(ProjModel _projModel);
    }
}
