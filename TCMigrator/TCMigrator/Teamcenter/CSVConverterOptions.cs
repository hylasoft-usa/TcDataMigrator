using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.Teamcenter
{
    public class CSVConverterOptions
    {
        public string SourceSite { get; set; }
        public string Encoding { get; set; }
        public char gmsTcxmlStringSeperator { get; set; }
        public bool skipExisting { get; set; }
        public string skipExistingType { get; set; }
        public bool groupDataItems { get; set; }
        public string groupDataItemsType { get; set; }
        public bool saveGsidOut { get; set; }
        public int islandSize { get; set; }
        public bool useLocalTime { get; set; }
        public int localTimeOffsetHours { get; set; }
        public bool lovValidate { get; set; }
        public bool useBvrPercise { get; set; }
        public string bv_type { get; set; }
        public string bvr_type { get; set; }
        public char csvSeperator { get; set; }
        public char quotationMarkIdentifier { get; set; }
        public char escapeChar { get; set; }
        public CSVConverterOptions()
        {
            //add defaults from settings file
        }
    }
}
