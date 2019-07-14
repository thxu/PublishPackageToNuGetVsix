using System.Collections.Generic;
using NuGet.Packaging;

namespace PublishPackageToNuGet2017.Model
{
    public class ProjModel
    {
        public string Key { get; set; }

        public string LibName { get; set; }

        public string LibDebugPath { get; set; }

        public string LibReleasePath { get; set; }

        public string Version { get; set; }

        public string Desc { get; set; }

        public string Author { get; set; }

        public IEnumerable<string> Owners { get; set; }

        public ManifestMetadata PackageInfo { get; set; }

        public string NetFrameworkVersion { get; set; }
    }
}
