using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigration.DataModel
{
    public class DatasetData
    {
        public String ParentId { get; set; }
        public String ParentType { get; set; }
        public String ParentRevisionId { get; set; }
        public String ParentRevisionType { get; set; }
        public String DatasetType { get; set; }
        public String NewFileName { get; set; }
        public String OriginalFileName { get; set; }
        public String VolumeTag { get; set; }
        public String SdPathName { get; set; }
        public String DatasetObjectName { get; set; }
        public String RelationType { get; set; }
        public bool UsesParentRevision { get
            {
                return (!String.IsNullOrWhiteSpace(ParentRevisionId) && !String.IsNullOrWhiteSpace(ParentRevisionType));
            }
        }
        public DatasetData(string pid, string pt, string dst, string nfn, string ofn, string vt, string sdpn, string dson, string rel, string prid="", string prt = "")
        {
            ParentType = pt;
            ParentId = pid;
            DatasetType = dst;
            DatasetObjectName = dson;
            NewFileName = nfn;
            OriginalFileName = ofn;
            VolumeTag = vt;
            SdPathName = sdpn;
            ParentRevisionId = prid;
            ParentRevisionType = prt;
            RelationType = rel;
        }
    }
}
