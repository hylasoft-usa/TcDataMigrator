using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Data;
using TCMigrator.Interfaces;

namespace TCMigrator.CSV
{
    public class GenericCsv : ICsv
    {
        private char separator;
        public GenericCsv()
        {
            this.separator = Properties.CSVSettings.Default.CSVSeparator;
        }

        public void SetSeparator(char sep)
        {
            this.separator = sep;
        }

        public string Write(ImportData data)
        {
            var csvContent = buildCsv(data);
            var dir = Properties.CSVSettings.Default.CSVDirectory + data.InputTitle + @"\";
            var fullName = dir + Properties.CSVSettings.Default.DefaultCSVName;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(fullName, csvContent);
            return fullName;
        }
        private String buildCsv(ImportData d) 
        {
            var lines = d.Entries;
            StringBuilder b = new StringBuilder();
            foreach(string s in d.Headers)
            {
                b.Append(s + this.separator);
            }
            b.Remove(b.Length - 1, 1);
            b.Append(Environment.NewLine);
            foreach (String[] line in lines)
            {
                foreach (string s in line)
                {
                    b.Append(s + this.separator);
                }
                b.Remove(b.Length - 1, 1); //remove last seperator
                b.Append(System.Environment.NewLine);
            }
            return b.ToString();
        }
    }
}
