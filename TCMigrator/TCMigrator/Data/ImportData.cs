using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.Data
{
    public class ImportData
    {
        String inputTitle;
        List<String> headers;
        List<String[]> entries;
        List<String> columnNames;
        bool areHeadersSet;
        private bool _areEntriesSplit;
        private bool _areEntriesFiltered;
        public List<String> Headers { get { return this.headers; } set { this.headers = value; if (this.headers.Count > 0) { this.areHeadersSet = true; } } }
        public List<String[]> Entries { get { return this.entries; } set { this.entries = value; } }
        public String InputTitle { get { return this.inputTitle; } }
        public List<String> ColumnNames { get { return columnNames; } set { this.columnNames = value; } }
        public bool AreHeadersSet { get { return this.areHeadersSet; } }
        public string csvLocation { get; set; }
        public bool AreEntriesSplit { get { return this._areEntriesSplit; } }
        public bool AreEntriesFiltered { get { return this._areEntriesFiltered; } }
        public List<List<String[]>> SplitEntries { get;}
        public List<List<String[]>> FilteredEntries { get;}
        public ImportData(String inputTitle)
        {
            this.FilteredEntries = new List<List<String[]>>();
            this.SplitEntries = new List<List<String[]>>();
            this._areEntriesFiltered = false;
            this._areEntriesSplit = false;
            this.inputTitle = inputTitle;
            headers = new List<String>();
            entries = new List<String[]>();
            areHeadersSet = false;
            csvLocation = null;
        }
        public void AddFilteredEntryList(List<string[]> entrySet)
        {
            this._areEntriesFiltered = true;
            this.FilteredEntries.Add(entrySet);
        }
        public void AddSplitEntrySet(List<String[]> entrySet)
        {
            this._areEntriesSplit = true;
            this.SplitEntries.Add(entrySet);
        }
    }
}
