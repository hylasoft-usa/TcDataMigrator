using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigration.Util
{
    public static class StaticUtilities
    {
        public static void checkCreateDir(String dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        public static string formatPath(string path)
        {
            if (path.EndsWith(@"\"))
            {
                return path;
            }
            return path + @"\";
        }

    }
}
