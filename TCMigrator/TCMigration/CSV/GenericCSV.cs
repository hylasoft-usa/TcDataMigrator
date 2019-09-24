using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCDataUtilities.DataModel;
using TCMigration.Util;

namespace TCDataUtilities.CSV
{
    public class GenericCSV : ICSV
    {
        private readonly string FilterFolder = @"FilteredEntries\";
        private char separator;
        private String defaultCsvName;
        public GenericCSV(char sep,string defaultCsvName="import.csv")
        {
            this.separator = sep;
            this.defaultCsvName = defaultCsvName;
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
        public string Write(ImportData data, String writeFolder)
        {
            var path = StaticUtilities.formatPath(writeFolder);
            var dir = path+ data.InputTitle + @"\";
            StaticUtilities.checkCreateDir(dir);
            WriteFilteredEntries(data,dir);
            WriteSplitEntries(data,dir);
            WriteEntries(data,dir);          
            return dir;
        }
        public void WriteEntries(ImportData data,String writeLocation)
        {
            if (!data.AreEntriesSplit)
            {
                var csvContent = buildCsv(data.Headers, data.Entries);
                var fullName = String.Format("{0}{1}.csv", writeLocation, defaultCsvName);
                File.WriteAllText(fullName, csvContent);
            }
        }
        private void WriteSplitEntries(ImportData data, String writeLocation)
        {
            if (data.AreEntriesSplit)
            {
                var splitEntries = data.SplitEntries;
                for (var x = 0; x < splitEntries.Count; x++)
                {
                    var csvContent = buildCsv(data.Headers, data.SplitEntries[x]);
                    var name = String.Format("{0}{1}_SPLIT_ENTRY_{2}.csv", writeLocation, defaultCsvName, x+1); 
                    File.WriteAllText(name, csvContent);
                }
            }

        }
        private void WriteFilteredEntries(ImportData data,String writeLocation)
        {
            var writeDir = StaticUtilities.formatPath(writeLocation);
            writeDir += FilterFolder;
            if (data.FilteredEntries != null && data.FilteredEntries.Count > 0)
            {
                StaticUtilities.checkCreateDir(writeDir);
                for(var x = 0; x < data.FilteredEntries.Count; x++)
                {
                    var filtered = data.FilteredEntries[x];
                    if (filtered.Count > 0)
                    {
                        var csvContent = buildCsv(data.Headers, filtered);
                        var fullName = String.Format("{0}{1}_FILTERED_ENTRIES_{2}.csv", writeDir, defaultCsvName, x);
                        File.WriteAllText(fullName, csvContent);
                    }
                }
            }
        }
        private String buildCsv(List<String> headers, List<String[]> entries)
        {
            var lines = entries;
            StringBuilder b = new StringBuilder();
            foreach (string s in headers)
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
