using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Data;
using TCMigrator.Interfaces;

namespace TCMigrator.Transform
{
    public class GenericTransformer : ITransformer
    {
        public ImportData replace(ImportData data, TransformOptions options, Action<string> callback = null)
        {
            if (options.ReplacementDictionary.Count < 1) { return data; }
            List<String[]> entries = data.Entries;
            Dictionary<String, String> replacementDictionary = options.ReplacementDictionary;
            List<String[]> transformedList = new List<String[]>();
            var totalReplacements = 0;
            for (var x = 0; x < entries.Count; x++)
            {
                List<String> fixedArray = new List<String>();
                foreach (string s in entries[x])
                {
                    var replacementCount = 0;
                    foreach (KeyValuePair<string, string> kvp in replacementDictionary)
                    {
                        if (s.Contains(kvp.Key)) { replacementCount++; }
                        fixedArray.Add(s.Replace(kvp.Key, kvp.Value));
                    }
                    totalReplacements += replacementCount;
                }
                transformedList.Add(fixedArray.ToArray());
                
            }
            data.Entries = transformedList;
            return data;
        }

        public ImportData remove(ImportData data, TransformOptions options, Action<string> updatefunction = null)
        {
            List<String[]> entries = data.Entries;
            List<String> remove = options.RemovalList;
            List<String[]> returnable = new List<String[]>();
            for (var x = 0; x < entries.Count; x++)
            {
                var entry = entries[x];
                List<string> arr = new List<string>();
                foreach (String s in entry)
                {
                    string val = s;
                    foreach (string removal in remove)
                    {
                        if (s.Contains(removal))
                        {
                            val = s.Remove(s.IndexOf(removal), removal.Length);
                        }
                    }
                    arr.Add(val);
                }
                returnable.Add(arr.ToArray());
            }
            data.Entries = returnable;
            return data;
        }

        public ImportData transform(ImportData data, TransformOptions options, Action<string> updateFunction = null)
        {
            ImportData d = replace(data, options, updateFunction);
            d = remove(d, options, updateFunction);
            d= trim(d, options);
            if (options.AreEntriesSplit)
            {
                d=splitEntries(d, options);
            }
            return d;
        }
        private ImportData splitEntries(ImportData d, TransformOptions o)
        {
            List<List<string[]>> splitEntries = new List<List<string[]>>();
            List<string[]> currentEntry = new List<String[]>();
            d.AreEntriesSplit = true;
            var rowCount = o.RowsPerFile;
            for(var x = 0; x < d.Entries.Count; x++)
            {
                if (x + 1 % rowCount == 0)
                {
                    splitEntries.Add(currentEntry);
                    currentEntry = new List<String[]>();
                }
                else
                {
                    currentEntry.Add(d.Entries[x]);
                }
            }
            if (currentEntry.Count != 0)
            {
                splitEntries.Add(currentEntry);
            }
            d.SplitEntries = splitEntries;
            return d;

        }

        public ImportData trim(ImportData data, TransformOptions options)
        {
            var entries = data.Entries;
            List<String[]> returnable = new List<String[]>();
            foreach (String[] s in entries)
            {
                var arr = new List<string>();
                foreach (string st in s)
                {
                    arr.Add(st.Trim());
                }
                returnable.Add(arr.ToArray());
            }
            data.Entries = returnable;
            return data;
        }
    }
}
