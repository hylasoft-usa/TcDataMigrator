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
        public List<String> Headers { get { return this.headers; } set { this.headers = value; if (this.headers.Count > 0) { this.areHeadersSet = true; } } }
        public List<String[]> Entries { get { return this.entries; } set { this.entries = value; } }
        public String InputTitle { get { return this.inputTitle; } }
        public List<String> ColumnNames { get { return columnNames; } set { this.columnNames = value; } }
        public bool AreHeadersSet { get { return this.areHeadersSet; } }
        public string csvLocation { get; set; }
        public bool AreEntriesSplit { get; set; }
        public List<List<String[]>> SplitEntries { get; set; }
        public ImportData(String inputTitle)
        {
            this.inputTitle = inputTitle;
            headers = new List<String>();
            entries = new List<String[]>();
            areHeadersSet = false;
            csvLocation = null;
        }
    }
}
