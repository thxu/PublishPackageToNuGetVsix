using System;

namespace PublishPackageToNuGet2017.Model
{
    public class SimplePkgView
    {
        public string Id { get; set; }

        public string Version { get; set; }

        public string Desc { get; set; }

        public string Author { get; set; }

        public DateTimeOffset? PublishDateTime { get; set; }

        public long DownloadCount { get; set; }
    }
}
