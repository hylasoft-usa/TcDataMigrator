﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCDataUtilities.DataModel;
using TCDataUtilities.Filter;
using TCMigration.Filter;
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
            d = performCompoundFiltering(d, options);
            d = performComparisonFiltering(d, options);
            d = filterEntries(d, options);
            if (options.AreEntriesSplit)
            {
                d=splitEntries(d, options);
            }
            return d;
        }
        private ImportData performCompoundFiltering(ImportData d, TransformOptions to)
        {
            if (to.CompoundFilters != null && to.CompoundFilters.Count > 0)
            {
                foreach (CompoundFilter f in to.CompoundFilters)
                {
                    var dataset = new List<string[]>();
                    List<string[]> matches = new List<string[]>();
                    d.Entries = f.Filter(d.Entries, out matches, true);
                    d.AddFilteredEntryList(matches);
                }
                return d;
            }
            return d;
        }
        private ImportData performComparisonFiltering(ImportData d, TransformOptions to)
        {
            if(to.ComparisonFilters!=null && to.ComparisonFilters.Count > 0)
            {
                foreach (ComparisonFilter f in to.ComparisonFilters)
                {
                    List<String[]> thisSet = new List<String[]>();
                    for (var x = 0; x < d.Entries.Count; x++)
                    {
                        var entry = d.Entries[x];
                        if (f.isMatch(entry))
                        {
                            thisSet.Add(entry);
                        }
                    }
                    d.AddFilteredEntryList(thisSet);
                    foreach (string[] s in thisSet)
                    {
                        d.Entries.Remove(s);
                    }
                }
                return d;
            }
            return d;
        }
        private ImportData splitEntries(ImportData d, TransformOptions o)
        {
            List<string[]> currentEntry = new List<String[]>();
            var rowCount = o.RowsPerFile;
            for(var x = 0; x < d.Entries.Count; x++)
            {
                currentEntry.Add(d.Entries[x]);
                if (x + 1 % rowCount == 0)
                {
                    d.AddSplitEntrySet(currentEntry);
                    currentEntry = new List<String[]>();
                }
            }
            if (currentEntry.Count != 0)
            {
                d.SplitEntries.Add(currentEntry);
            }
            return d;

        }
        private ImportData filterEntries(ImportData d, TransformOptions o)
        {
            foreach(ColumnFilter f in o.ColumnFilters)
            {
                List<String[]> thisSet = new List<String[]>();
                for (var x = 0; x < d.Entries.Count; x++)
                {
                    var entry = d.Entries[x];
                    if (f.isMatch(entry))
                    {
                        thisSet.Add(entry);
                    }
                }
                d.AddFilteredEntryList(thisSet);
                foreach (string[] s in thisSet) {
                    d.Entries.Remove(s);
                        }
            }
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
