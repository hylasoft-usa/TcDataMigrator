using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigration.DataModel
{
    public class RelationDataMap
    {
        private List<string> _columns;
        public int ParentIdIndex = -1;
        public int ParentTypeIndex = -1;
        public int ParentRevIdIndex = -1;
        public int ParentRevTypeIndex = -1;
        public int ChildIdIndex = -1;
        public int ChildTypeIndex = -1;
        public int ChildRevIdIndex = -1;
        public int ChildRevTypeIndex = -1;
        public int RelationTypeIndex = -1;
        public bool useParentRev { get { return ParentRevIdIndex > -1 && ParentRevTypeIndex > -1; } }
        public bool useChildRev { get { return ChildRevIdIndex > -1 && ChildRevTypeIndex > -1; } }

        public RelationDataMap(List<string> tablecolumns, int parentId, int parentType, int childId, int childType, int relationType, int parentRevId = -1, int parentRevType = -1, int childRevId = -1, int childRevType = -1)
        {
            _columns = tablecolumns;
            ParentIdIndex = parentId;
            ParentTypeIndex = parentType;
            ParentRevTypeIndex = parentRevType;
            ParentRevIdIndex = parentRevId;
            ChildIdIndex = childId;
            ChildTypeIndex = childType;
            ChildRevIdIndex = childRevId;
            ChildRevTypeIndex = childRevType;
            RelationTypeIndex = relationType;
        }
    }
}
