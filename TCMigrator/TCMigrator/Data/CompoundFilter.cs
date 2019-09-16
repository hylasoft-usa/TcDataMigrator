using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Enums;

namespace TCMigrator.Data
{
    public class CompoundFilter
    {
        List<ColumnFilter> filters;
        CompoundFilterType filterType;
        public CompoundFilterType JoinType { get { return this.filterType; } }
        public int FilterCount { get { return filters.Count; } }

        public CompoundFilter(CompoundFilterType type) {
            filters = new List<ColumnFilter>();
            filterType = type;
        }
        public void RemoveFilter(ColumnFilter filter)
        {
            for(var x = 0; x < filters.Count; x++)
            {
                if (filter == filters[x])
                {
                    RemoveFilter(x);
                }
            }
        }
        public void RemoveFilter(int filterIndex)
        {
            filters.RemoveAt(filterIndex);
        }
        public void addFilter(ColumnFilter f)
        {
            filters.Add(f);
        }
        public List<String[]> Filter(List<String[]> data,out List<string[]> matches,bool removeFromDataset=true)
        {
            var myData = data;
            matches = new List<String[]>();
            foreach(ColumnFilter f in filters)
            {
                switch (filterType)
                {
                    case CompoundFilterType.AND:
                        matches = AndFilter(myData);
                        break;
                    case CompoundFilterType.AND_NOT:
                        matches = AndNotFilter(myData);
                        break;
                    case CompoundFilterType.OR:
                        matches = OrFilter(myData);
                        break;
                    case CompoundFilterType.OR_NOT:
                        matches = OrNotFilter(myData);
                        break;
                }
            }
            if (removeFromDataset)
            {
                return RemoveMatching(myData, matches);
            }
            else
            {
                return myData;
            }
        }
        private List<String[]> RemoveMatching(List<String[]> data, List<String[]> matches)
        {
            foreach(string[] match in matches)
            {
                data.Remove(match);
            }
            return data;
        }
        private List<String[]> AndFilter(List<String[]> data)
        {
            List<string[]> matches = new List<string[]>();
            foreach (string[] d in data)
            {
                bool shouldAdd = true;
                foreach (ColumnFilter f in filters)
                {
                    if (!f.isMatch(d)) { shouldAdd = false; }
                }
                if (shouldAdd) { matches.Add(d); }
            }
            return matches;
        }
        private List<String[]> AndNotFilter(List<String[]> data)
        {
            List<string[]> matches = new List<string[]>();
            foreach (string[] d in data)
            {
                bool shouldAdd = true;
                foreach (ColumnFilter f in filters)
                {
                    if (f.isMatch(d)) { shouldAdd = false; }
                }
                if (shouldAdd) { matches.Add(d); }
            }
            return matches;
        }
        private List<String[]> OrFilter(List<String[]> data)
        {
            List<string[]> matches = new List<string[]>();
            foreach (string[] d in data)
            {
                bool shouldAdd = false;
                foreach (ColumnFilter f in filters)
                {
                    if (f.isMatch(d)) { shouldAdd = true; }
                }
                if (shouldAdd) { matches.Add(d); }
            }
            return matches;
        }
        private List<String[]> OrNotFilter(List<String[]> data) {
            List<string[]> matches = new List<string[]>();
            foreach (string[] d in data)
            {
                bool shouldAdd = true;
                foreach (ColumnFilter f in filters)
                {
                    if (f.isMatch(d)) { shouldAdd = false; }
                }
                if (shouldAdd) { matches.Add(d); }
            }
            return matches;
        }

    }
}
