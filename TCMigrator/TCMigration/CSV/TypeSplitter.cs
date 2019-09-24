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
        public List<List<string[]>> SplitForDatasets(int dsIndex, int parentIndex, int relTypeIndex,List<String[]> entries)
        {
            List<List<string[]>> combos = new List<List<string[]>>();
            List<string> uniqueDatasetTypes = getUniqueAtIndex(dsIndex,entries);
            List<string> uniqueItemTypes = getUniqueAtIndex(parentIndex, entries);
            List<string> uniqueRelationTypes = getUniqueAtIndex(relTypeIndex, entries);
            foreach(String s in uniqueDatasetTypes)
            {
                foreach(String st in uniqueItemTypes)
                {
                    foreach(String str in uniqueRelationTypes)
                    {
                        List<string[]> matches = entries.Where(o => o[dsIndex] == s && o[parentIndex] == st && o[relTypeIndex] == str).ToList();
                        if (matches!=null&&matches.Count > 0)
                        {
                            combos.Add(matches);
                        }
                    }
                }
            }
            return combos;

        }
        private List<string> getUniqueAtIndex(int index, List<string[]> data)
        {
            return data.Select(o => o[index]).Distinct().ToList();
        }
        public List<List<string[]>> SplitForRelations(List<RelationData> data)
        {
            List<List<String[]>> allsets = new List<List<string[]>>();
            var parents = getDistinctParentTypes(data);
            foreach (string s in parents)
            {
                var children = getChildTypesFromParent(data, s);
                foreach (string st in children)
                {
                    List<String[]> datasets = new List<String[]>();
                    var sub = getEntrySet(data, s, st);
                    datasets.Add(getHeader(sub[0]));
                    foreach (RelationData d in sub)
                    {
                        datasets.Add(getEntry(d));
                    }
                    allsets.Add(datasets);
                }
            }
            return allsets;
        }
        private List<string> getDistinctParentTypes(List<RelationData> d)
        {
            if (d[0].UsesParentRevision)
            {
                return d.Select(o => o.ParentRevType).Distinct().ToList();
            }
            else
            {
                return d.Select(o => o.ParentType).Distinct().ToList();
            }
        }
        private List<string> getChildTypesFromParent(List<RelationData> d, String ParentType)
        {
            List<RelationData> ds = d.Where(o => o.ParentType == ParentType).ToList();
            if (d[0].UsesParentRevision)
            {
                ds = d.Where(o => o.ParentRevType == ParentType).ToList();
            }
            if (d[0].UsesChildRevision)
            {
                return ds.Select(o => o.ChildRevType).Distinct().ToList();
            }
            return ds.Select(o => o.ChildType).Distinct().ToList();
        }
        private List<RelationData> getEntrySet(List<RelationData> d, string parent, string child)
        {
            var e1 = d[0];
            List<RelationData> f1 = e1.UsesParentRevision ? d.Where(x => x.ParentRevType == parent).ToList() : d.Where(x => x.ParentType == parent).ToList();
            return e1.UsesChildRevision ? f1.Where(x => x.ChildRevType == child).ToList() : f1.Where(x => x.ChildType == child).ToList();
        }
        private string[] getHeader(RelationData d)
        {
            List<string> headers = new List<string>();
            headers.Add(String.Format("{0}[1]:item_id", d.ParentType));

            var cid = String.Format("{0}[2]:item_id", d.ChildType);
            string relation = "ImanRelation:{0}(primary_object->{1}[1];secondary_object->{2}[2]";
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
