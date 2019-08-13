using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.Helpers
{
    public static class PropertyHelper
    {
        public static string ConvertPropertyNameToLabel(string name)
        {
            var parts = name.Split('_');
            string newName = "";
            foreach (string s in parts)
            {
                newName += s + " ";
            }
            return newName.TrimEnd(' ');
        }
        public static string convertLabelToPropertyName(string labelContent)
        {
            var parts = labelContent.Split(' ');
            var name = "";
            foreach (string s in parts)
            {
                name += s + "_";
            }
            return name.Substring(0, name.Length - 1);
        }
    }
}
