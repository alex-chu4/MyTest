using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Path
{
    public static class PathService
    {
        public static string UpLevel(this string path, int level)
        {
            var directory = File.GetAttributes(path).HasFlag(FileAttributes.Directory)
                ? path
                : System.IO.Path.GetDirectoryName(path);

            level = level < 0 ? 0 : level;

            for (var i = 0; i < level; i++)
            {
                directory = System.IO.Path.GetDirectoryName(directory);
            }

            return directory;
        }
    }
}
