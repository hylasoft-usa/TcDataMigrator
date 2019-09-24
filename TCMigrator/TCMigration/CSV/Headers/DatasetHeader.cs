using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCDataUtilities.CSV.Headers
{
    public class DatasetHeader:IHeader
    {
        private HeaderSpecification Parent;
        private HeaderSpecification DatasetType;
        private HeaderSpecification DatasetObjectName;
        private HeaderSpecification ImanFileName;
        private HeaderSpecification ImanOriginalFileName;
        private HeaderSpecification ImanVolumeTag;
        private HeaderSpecification ImanSdPathName;
        private HeaderSpecification ParentRevision;
        private HeaderSpecification RelationType;
        private bool _useRevision;
        private readonly string BasicHeaderItem = "{0}:{1}";
        private readonly string NamedItem = "{0}[{1}]:{2}";
        private readonly string RelationTag = "{0}[{1}]{2}(<-{3}<-{4})";
        public String ItemType { get { return Parent.ItemType; } }
        public String DataSetType { get { return DatasetType.ItemType; } }
        public String RelationshipType { get { return RelationType.ItemType; } }
        public DatasetHeader(HeaderSpecification parent, HeaderSpecification datasetType, HeaderSpecification DSObjectName, HeaderSpecification IFFN,HeaderSpecification IFOFN, HeaderSpecification IFVolume, HeaderSpecification IFSdPath, HeaderSpecification relType,HeaderSpecification rev=null)
        {
            Parent = parent;
            DatasetType = datasetType;
            DatasetObjectName = DSObjectName;
            ImanFileName = IFFN;
            ImanOriginalFileName = IFOFN;
            ImanVolumeTag = IFVolume;
            ImanSdPathName = IFSdPath;
            RelationType = relType;
            if (rev != null)
            {
                _useRevision = true;
                ParentRevision = rev;
            }
        }

        public List<String> getHeader() { 
                List<String> HeaderList = new List<String>();
                HeaderList.Add(String.Format(BasicHeaderItem,Parent.ItemType,Parent.HeaderText));
                if (_useRevision)
                {
                    HeaderList.Add(String.Format(BasicHeaderItem, ParentRevision.ItemType, ParentRevision.HeaderText));
                }
                HeaderList.Add(String.Format(NamedItem, DatasetType.ItemType, 1, DatasetType.HeaderText));
                if (_useRevision)
                {
                    HeaderList.Add(String.Format(RelationTag, DatasetObjectName.ItemType, 1, DatasetObjectName.HeaderText, RelationType.HeaderText, ParentRevision.ItemType));
                }
                else
                {
                    HeaderList.Add(String.Format(RelationTag, DatasetObjectName.ItemType, 1, DatasetObjectName.HeaderText, RelationType.HeaderText, Parent.ItemType));
                }
                HeaderList.Add(String.Format(NamedItem, ImanFileName.ItemType, 1, ImanFileName.HeaderText));
                HeaderList.Add(String.Format(NamedItem, ImanOriginalFileName.ItemType, 1, ImanOriginalFileName.HeaderText));
                HeaderList.Add(String.Format(NamedItem, ImanVolumeTag.ItemType, 1, ImanVolumeTag.HeaderText));
                HeaderList.Add(String.Format(NamedItem, ImanSdPathName.ItemType, 1, ImanSdPathName.HeaderText));
                return HeaderList;
            }
    }
}
