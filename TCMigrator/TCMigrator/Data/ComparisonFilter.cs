using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Enums;

namespace TCMigrator.Data
{
    public class ComparisonFilter
    {
        private int index1;
        private int index2;
        private String col1Name;
        private String col2Name;
        private FilterType filterType;
        public int IndexA { get { return index1; } }
        public int IndexB { get { return index2; } }
        public string ColumnAName { get { return col1Name; } }
        public string ColBName { get { return col2Name; } }
        public FilterType CompareType { get { return filterType; } }
        public ComparisonFilter(int index1, int index2, string col1Name, string col2Name, FilterType compareType)
        {
            this.index1 = index1;
            this.index2 = index2;
            this.col1Name = col1Name;
            this.col2Name = col2Name;
            this.filterType = compareType;
        }
        public bool isMatch(string[] data)
        {
            bool isMatch = false;
            switch (filterType)
            {
                case FilterType.EQUAL_TO:
                    isMatch = IsEqualTo(data);
                    break;
                case FilterType.GREATER_THAN:
                    isMatch = IsGreaterThan(data);
                    break;
                case FilterType.LESS_THAN:
                    isMatch = IsLessThan(data);
                    break;
                case FilterType.GREATER_THAN_OR_EQUAL_TO:
                    isMatch = IsGreaterThanOrEqual(data);
                    break;
                case FilterType.LESS_THAN_OR_EQUAL_TO:
                    isMatch = IsLessThanOrEqual(data);
                    break;
                case FilterType.BEFORE:
                    isMatch = IsBefore(data);
                    break;
                case FilterType.AFTER:
                    isMatch = IsAfter(data);
                    break;
            }
            return isMatch;
        }

        private bool IsBefore(string[] data)
        {
            DateTime d1;
            if (DateTime.TryParse(data[index1], out d1))
            {
                DateTime d2;
                if (DateTime.TryParse(data[index2], out d2))
                {
                    return d1 < d2;
                }
                return false;
            }
            return false;
        }
        private bool IsAfter(string[] data)
        {
            DateTime d1;
            if (DateTime.TryParse(data[index1], out d1))
            {
                DateTime d2;
                if (DateTime.TryParse(data[index2], out d2))
                {
                    return d1 > d2;
                }
                return false;
            }
            return false;
        }

        private bool IsEqualTo(string[] data)
        {
            return data[index1] == data[index2];
        }
        private bool IsGreaterThan(string[] data)
        {
            long val = 0;
            if (Int64.TryParse(data[index1], out val))
            {
                long comp = 0;
                if (Int64.TryParse(data[index2], out comp))
                {
                    return comp > val;
                }
                return false;
            }
            return false;
        }
        private bool IsLessThan(string[] data)
        {
            long val = 0;
            if (Int64.TryParse(data[index1], out val))
            {
                long comp = 0;
                if (Int64.TryParse(data[index2], out comp))
                {
                    return comp < val;
                }
                return false;
            }
            return false;
        }
        private bool IsLessThanOrEqual(string[] data)
        {
            long val = 0;
            if (Int64.TryParse(data[index1], out val))
            {
                long comp = 0;
                if (Int64.TryParse(data[index2], out comp))
                {
                    return comp <= val;
                }
                return false;
            }
            return false;
        }
        private bool IsGreaterThanOrEqual(string[] data)
        {
            long val = 0;
            if (Int64.TryParse(data[index1], out val))
            {
                long comp = 0;
                if (Int64.TryParse(data[index2], out comp))
                {
                    return comp >= val;
                }
                return false;
            }
            return false;
        }
    }
}
