using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GarminExport
{
    public static class FileUtils
    {
        public static DirectoryInfo CreateDirectoryIfNotExists(string path)
        {
            if (!path.EndsWith("/"))
                path += "/";

            DirectoryInfo targetDirectory = new DirectoryInfo(path);
            if (!targetDirectory.Exists)
                targetDirectory.Create();
            return targetDirectory;
        }
    }
}
