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
        private readonly string FilterFolder = @"FilteredEntries\";
        private char separator;
        public GenericCsv()
        {
            this.separator = Properties.CSVSettings.Default.CSVSeparator;
        }

        public void SetSeparator(char sep)
        {
            this.separator = sep;
        }
        /// <summary>
        /// Writes CSV file(s) to the specified location in ImportData
        /// </summary>
        /// <param name="data">Formatted Import Data Object with proper Tranforms already complete</param>
        /// <returns>If Entries are Split, returns the Directory containing written files, else returns the full file path to the single csv</returns>
        public string Write(ImportData data)
        {
            var dir = Properties.CSVSettings.Default.CSVDirectory + data.InputTitle + @"\";
            var pathToReturn="";
            if (!Directory.Exists(dir+FilterFolder))
            {
                Directory.CreateDirectory(dir+FilterFolder);
            }
            if(data.FilteredEntries!=null && data.FilteredEntries.Count > 0)
            {
                int x = 1;
                foreach(List<String[]> filtered in data.FilteredEntries)
                {
                    if (filtered.Count > 0)
                    {
                        var csvContent = buildCsv(data.Headers, filtered);
                        var fullName = dir +FilterFolder+ Properties.CSVSettings.Default.DefaultCSVName + "_FILTERED_ENTRIES_" + x + ".csv";
                        File.WriteAllText(fullName, csvContent);
                        pathToReturn = dir;
                        x++;
                    }
                }
            }
            if (!data.AreEntriesSplit)
            {
                var csvContent = buildCsv(data.Headers,data.Entries);
                var fullName = dir + Properties.CSVSettings.Default.DefaultCSVName+".csv";
                File.WriteAllText(fullName, csvContent);
                pathToReturn = dir;
            }
            else
            {
                var filteredDir = dir + FilterFolder;
                if (!Directory.Exists(filteredDir))
                {
                    Directory.CreateDirectory(filteredDir);
                }
                var splitEntries = data.SplitEntries;
                for(var x=0; x < splitEntries.Count; x++)
                {
                    var csvContent = buildCsv(data.Headers, data.SplitEntries[x]);
                    var name = dir + Properties.CSVSettings.Default.DefaultCSVName + "_SPLIT_ENTRIES_"+(x + 1) + ".csv";
                    File.WriteAllText(name, csvContent);
                    pathToReturn = dir;
                }
            }
            return pathToReturn;
        }
        private String buildCsv(List<String> headers,List<String[]>entries) 
        {
            var lines = entries;
            StringBuilder b = new StringBuilder();
            foreach(string s in headers)
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
