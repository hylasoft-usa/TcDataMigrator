using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigration.DataModel
{
    public class DatasetDataMap
    {
        public List<string> _columns;
        public int ParentIdIndex = -1;
        public int ParentTypeIndex = -1;
        public int ParentRevIdIndex = -1;
        public int ParentRevTypeIndex = -1;
        public int DatasetTypeIndex = -1;
        public int NewFileNameIndex = -1;
        public int OriginalFileNameIndex = -1;
        public int VolumeTagIndex = -1;
        public int SdPathIndex = -1;
        public int DatasetObjectNameIndex = -1;
        public int RelationTypeIndex = -1;
        public bool UsesParentRevision { get { return (ParentRevIdIndex > -1 && ParentRevTypeIndex > -1); } }

        public DatasetDataMap(List<string> tablecolumns,int pid,int ptid, int dsti, int nfni, int ofni,int vti, int sdpi,int dsoni,int reli, int prii=-1,int prti=-1)
        {
            _columns = tablecolumns;
            ParentIdIndex = pid;
            ParentTypeIndex = ptid;
            ParentRevIdIndex = prii;
            ParentRevTypeIndex = prti;
            DatasetTypeIndex = dsti;
            NewFileNameIndex = nfni;
            OriginalFileNameIndex = ofni;
            VolumeTagIndex = vti;
            SdPathIndex = sdpi;
            DatasetObjectNameIndex = dsoni;
            RelationTypeIndex = reli;
        }
    }
}
