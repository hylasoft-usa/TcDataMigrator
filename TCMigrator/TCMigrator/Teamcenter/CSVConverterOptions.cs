using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IM = TCMigrator.Properties.ImportSettings;

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
            SourceSite = IM.Default.SOURCE_SITE;
            Encoding = IM.Default.ENCODING;
            gmsTcxmlStringSeperator = IM.Default.GMS_TCXML_STRING_SEPERATOR;
            skipExisting = IM.Default.SKIP_EXISTING;
            skipExistingType = IM.Default.DEFAULT_SKIP_EXISTING_TYPE;
            groupDataItems = IM.Default.DEFAULT_GROUP_ITEMS;
            groupDataItemsType = IM.Default.DEFAULT_GROUP_ITEMS_TYPE;
            saveGsidOut = IM.Default.SAVE_GSID_OUT;
            islandSize = IM.Default.DEFAULT_ISLAND_SIZE;
            useLocalTime = IM.Default.DEFAULT_USE_LOCAL_TIME;
            localTimeOffsetHours = IM.Default.LOCAL_TIMEZONE_OFFSET_HOURS;
            lovValidate = IM.Default.LOV_VALIDATE;
            useBvrPercise = IM.Default.BVR_PERCISE;
            bv_type = IM.Default.BOMVIEW_TYPE;
            bvr_type = IM.Default.BOMVEIW_REVISION_TYPE;
            csvSeperator = IM.Default.CSV_SEPARATOR;
            quotationMarkIdentifier = IM.Default.CSV_QUOTATION;
            escapeChar = IM.Default.CSV_ESCAPE;
        }
    }
}
