using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Versioning;
using NuGet.Packaging;

namespace PublishPackageToNuGet.Model
{
    public class MyPackageFile : IPackageFile
    {
        private readonly Func<Stream> _streamFactory;

        public MyPackageFile(PackageArchiveReader reader, ZipArchiveEntry entry)
        {
            Path = UnescapePath(entry.FullName.Replace('/', '\\'));
            LastWriteTime = entry.LastWriteTime;
            _streamFactory = () => reader.GetStream(UnescapePath(entry.FullName));
        }

        public MyPackageFile(string folder, string name, string fullPath, string frameVersion)
        {
            OriginalPath = fullPath;
            Path = string.IsNullOrWhiteSpace(frameVersion) ? $"{folder}\\{name}" : $"{folder}\\{frameVersion}\\{name}";
            EffectivePath = name;
            TargetFramework = new FrameworkName(".NET Framework, Version=4.5");
            LastWriteTime = DateTimeOffset.Now;
        }

        public Stream GetStream()
        {
            return File.OpenRead(OriginalPath);
        }



        /// <summary>Gets the full path of the file inside the package.</summary>
        public string Path { get; }

        public string OriginalPath { get; set; }

        /// <summary>
        /// Gets the path that excludes the root folder (content/lib/tools) and framework folder (if present).
        /// </summary>
        /// <example>
        /// If a package has the Path as 'content\[net40]\scripts\jQuery.js', the EffectivePath
        /// will be 'scripts\jQuery.js'.
        /// If it is 'tools\init.ps1', the EffectivePath will be 'init.ps1'.
        /// </example>
        public string EffectivePath { get; }
        public FrameworkName TargetFramework { get; }
        public DateTimeOffset LastWriteTime { get; }


        private static string UnescapePath(string path)
        {
            if (path != null
                && path.IndexOf('%') > -1)
            {
                return Uri.UnescapeDataString(path);
            }

            return path;
        }
    }
}
