using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigration.DataModel
{
    public class RelationData
    {
        public string ParentType { get; set; }
        public string ParentId { get; set; }
        public string ParentRevType { get; set; }
        public string ParentRev { get; set; }
        public string ChildType { get; set; }
        public string ChildId { get; set; }
        public string ChildRevType { get; set; }
        public string ChildRev { get; set; }
        public string RelationType { get; set; }
        public bool UsesParentRevision
        {
            get
            {
                return (!String.IsNullOrEmpty(ParentRevType) && !String.IsNullOrEmpty(ParentRev));
            }
        }
        public bool UsesChildRevision
        {
            get
            {
                return (!String.IsNullOrEmpty(ChildRev) && !String.IsNullOrEmpty(ChildRevType));
            }
        }
        public RelationData(string pt, string pid, string prt, string pr, string ct, string cid, string crt, string cr, string rel)
        {
            ParentType = pt;
            ParentId = pid;
            ParentRevType = prt;
            ChildType = ct;
            ChildId = cid;
            ChildRevType = crt;
            ChildRev = cr;
            RelationType = rel;
        }
    }
}
