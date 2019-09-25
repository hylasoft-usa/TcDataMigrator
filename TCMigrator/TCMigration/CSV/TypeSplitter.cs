using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCDataUtilities.CSV.Headers;
using TCMigration.DataModel;

namespace TCDataUtilities.CSV
{
    public class TypeSplitter
    {
        public List<List<string[]>> SplitForDatasets(List<DatasetData> d)
        {
            List<List<string[]>> allsets = new List<List<string[]>>();
            //attaches to item
            var itemDs = d.Where(x => !x.UsesParentRevision).ToList();
            //attaches to Item Revision 
            var irDs = d.Where(x => x.UsesParentRevision);

            var p1 = itemDs.Select(x => x.ParentType).Distinct().ToList();
            var p2 = irDs.Select(x => x.ParentType).Distinct();
            foreach(string s in p1)
            {
                var s1 = itemDs.Where(x => x.ParentType == s);
                var d1 = s1.Select(x => x.DatasetType).Distinct().ToList();
                foreach(string str in d1)
                {
                    var s2 = s1.Where(x => x.DatasetType == str);
                    var r1 = s2.Select(x => x.RelationType).Distinct().ToList();
                    foreach(string stri in r1)
                    {
                        var s3 = s2.Where(x => x.RelationType == stri).ToList();
                        if(s3!=null && s3.Count > 0)
                        {
                            List<string[]> thisCsv = new List<string[]>();
                            thisCsv.Add(getDatasetHeader(s3[0]));
                            foreach(DatasetData ds3 in s3)
                            {
                                thisCsv.Add(getDatasetItem(ds3));
                            }
                        }
                    }
                }
            }

            var parents = d.Select(x => x.ParentType).Distinct();

            return allsets;

        }
        private string[] getDatasetHeader(DatasetData exampleObject)
        {
            var header = new List<String>();
            header.Add(String.Format("{0}:item_id", exampleObject.ParentType));
            header.Add(String.Format("Dataset[T]:dataset_type"));
            header.Add(String.Format("ImanFile[T]:file_name"));
            header.Add(String.Format("ImanFile[T]:original_file_name"));
            header.Add(String.Format("ImanFile[T]:volume_tag"));
            header.Add(String.Format("ImanFile[T]:sd_path_name"));
            if (exampleObject.UsesParentRevision)
            {
                header.Insert(1, String.Format("{0}:item_revision_id", exampleObject.ParentRevisionType));
                header.Insert(3, String.Format("Dataset[T]:object_name(<-{0}<-{1})", exampleObject.RelationType, exampleObject.ParentRevisionType));
            }
            else
            {
                header.Insert(3, String.Format("Dataset[T]:object_name(<-{0}<-{1})", exampleObject.RelationType, exampleObject.ParentType));
            }
            return header.ToArray();
        }
        private string[] getDatasetItem(DatasetData d)
        {
            List<string> data = new List<string>();
            data.Add(d.ParentId);
            if (d.UsesParentRevision) { data.Add(d.ParentRevisionId); }
            data.Add(d.DatasetType);
            data.Add(d.DatasetObjectName);
            data.Add(d.NewFileName);
            data.Add(d.OriginalFileName);
            data.Add(d.VolumeTag);
            data.Add(d.SdPathName);
            return data.ToArray();
        }
        private List<string> getUniqueAtIndex(int index, List<string[]> data)
        {
            return data.Select(o => o[index]).Distinct().ToList();
        }
        public List<List<string[]>> SplitForRelations(List<RelationData> data)
        {
            List<List<String[]>> allsets = new List<List<string[]>>();
            var parents = getDistinctParentTypes(data);
            foreach (string parentType in parents)
            {
                var parentSet = data.Where(x => x.ParentType == parentType).ToList();
                var children = getChildTypes(parentSet);
                foreach (string childType in children)
                {
                    var childSet = parentSet.Where(x => x.ChildType == childType).ToList();
                    if (childSet[0].UsesParentRevision)
                    {
                        var parentRevTypeSet = getDistinctParentRevTypes(childSet);
                        foreach(string parentRevType in parentRevTypeSet)
                        {
                            List<RelationData> parentRevSet = childSet.Where(x => x.ParentRevType == parentRevType).ToList();
                            if (childSet[0].UsesChildRevision)
                            {
                                var childRevTypes = getChildRevTypes(parentRevSet);
                                foreach(string childRevType in childRevTypes)
                                {
                                    var ds = parentRevSet.Where(x => x.ChildRevType == childRevType).ToList();
                                    List<string[]> entry = new List<string[]>();
                                    entry.Add(getHeader(ds[0]));
                                    foreach(RelationData item in ds)
                                    {
                                        entry.Add(getEntry(item));
                                    }
                                    allsets.Add(entry);
                                }
                            }
                        }
                    }else if (childSet[0].UsesChildRevision)
                    {
                        var childRevSet = getChildRevTypes(childSet);
                        foreach(string childRev in childRevSet)
                        {
                            var ds = childSet.Where(x => x.ChildRevType == childRev).ToList();
                            foreach(RelationData d in ds)
                            {
                                List<string[]> entry = new List<string[]>();
                                entry.Add(getHeader(ds[0]));
                                foreach (RelationData item in ds)
                                {
                                    entry.Add(getEntry(item));
                                }
                                allsets.Add(entry);
                            }

                        }
                    }
                    else
                    {
                        var entry = new List<string[]>();
                        entry.Add(getHeader(childSet[0]));
                        foreach(RelationData d in childSet)
                        {
                            entry.Add(getEntry(d));
                        }
                        allsets.Add(entry);
                    }
                }
            }
            return allsets;
        }
        private List<string> getDistinctParentTypes(List<RelationData> d)
        {
            if (d.Count > 0)
            {
                var items = d.Select(x => x.ChildType).Distinct().ToList();
                if (items.Count > 0)
                {
                    return items;
                }
                return new List<string>();
            }
            return new List<string>();
        }
        private List<string> getDistinctParentRevTypes(List<RelationData> d)
        {
            if (d.Count > 0)
            {
                List<string> revs = new List<string>();
                revs = d.Select(x => x.ParentRevType).Distinct().ToList();

                if (revs.Count > 0)
                {
                    return revs;
                }
                return new List<string>();
            }
            return new List<string>();
        }
        private List<string> getChildTypes(List<RelationData> d)
        {
            if (d.Count > 0)
            {
                return d.Select(o => o.ChildType).Distinct().ToList();
            }
            return new List<String>();
        }
        private List<string> getChildRevTypes(List<RelationData> d)
        {
            if (d.Count > 0)
            {
                return d.Select(x => x.ChildRevType).Distinct().ToList();
            }
            return new List<string>();
        }
        private List<RelationData> getEntrySet(List<RelationData> d, string parent, string child)
        {
            if (d.Count > 0)
            {
                var e1 = d[0];
                List<RelationData> f1 = e1.UsesParentRevision ? d.Where(x => x.ParentRevType == parent).ToList() : d.Where(x => x.ParentType == parent).ToList();
                return e1.UsesChildRevision ? f1.Where(x => x.ChildRevType == child).ToList() : f1.Where(x => x.ChildType == child).ToList();
            }
            return new List<RelationData>();
        }
        private string[] getHeader(RelationData d)
        {
            List<string> headers = new List<string>();
            headers.Add(String.Format("{0}[1]:item_id", d.ParentType));

            headers.Add(String.Format("{0}[2]:item_id", d.ChildType));
            string relation = "ImanRelation:{0}(primary_object->{1}[1];secondary_object->{2}[2])";
            string parentType = d.ParentType;
            string childType = d.ChildType;
            if (d.UsesParentRevision)
            {
                headers.Insert(1, String.Format("{0}[1]:item_revision_id", d.ParentRevType));
                parentType = d.ParentRevType;
            }
            if (d.UsesChildRevision)
            {
                headers.Add(String.Format("{0}[2]:item_revision_id", d.ChildRevType));
                childType = d.ChildRevType;
            }

            headers.Add(String.Format(relation, d.RelationType, parentType, childType));
            return headers.ToArray();

        }
        private string[] getEntry(RelationData d)
        {
            List<string> ds = new List<string>();
            ds.Add(d.ParentId);
            if (d.UsesParentRevision) { ds.Add(d.ParentRev); }
            ds.Add(d.ChildId);
            if (d.UsesChildRevision) { ds.Add(d.ChildRev); }
            ds.Add(d.RelationType);
            return ds.ToArray();
        }
    }
}
