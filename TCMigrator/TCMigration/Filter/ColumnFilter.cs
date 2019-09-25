using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCDataUtilities.Filter;

namespace TCMigration.Filter
{
    public class ColumnFilter
    {
        private int _index;
        private string _name;
        private FilterType _type;
        private string _filterValue;
        public int Index { get { return this._index; } }
        public string Name { get { return this._name; } }
        public FilterType Type { get { return this._type; } }
        public string FilterValue { get { return _filterValue; } }
        public ColumnFilter(int index, string name, FilterType type, string filterValue)
        {
            this._index = index;
            this._name = name;
            this._type = type;
            this._filterValue = filterValue;
        }
        public bool isMatch(string[] data)
        {
            bool isMatch = false;
            switch (Type)
            {
                case FilterType.CONTAINS:
                    isMatch = CheckContains(data);
                    break;
                case FilterType.STARTS_WITH:
                    isMatch = CheckStartsWith(data);
                    break;
                case FilterType.ENDS_WITH:
                    isMatch = CheckEndsWith(data);
                    break;
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
                case FilterType.IS_EMPTY:
                    isMatch = IsEmpty(data);
                    break;
                case FilterType.IS_NULL:
                    isMatch = IsNull(data);
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
        private bool IsEmpty(string[] data)
        {
            return String.IsNullOrEmpty(data[Index]);
        }
        private bool IsNull(string[] data)
        {
            return data[Index] == null;
        }
        private bool IsBefore(string[] data)
        {
            DateTime d1;
            if (DateTime.TryParse(data[Index], out d1))
            {
                DateTime d2;
                if (DateTime.TryParse(FilterValue, out d2))
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
            if (DateTime.TryParse(data[Index], out d1))
            {
                DateTime d2;
                if (DateTime.TryParse(FilterValue, out d2))
                {
                    return d1 > d2;
                }
                return false;
            }
            return false;
        }
        private bool CheckContains(string[] data)
        {
            return data[Index].Contains(FilterValue);
        }
        private bool CheckStartsWith(string[] data)
        {
            return data[Index].StartsWith(FilterValue);
        }
        private bool CheckEndsWith(string[] data)
        {
            return data[Index].EndsWith(FilterValue);
        }
        private bool IsEqualTo(string[] data)
        {
            return data[Index] == FilterValue;
        }
        private bool IsGreaterThan(string[] data)
        {
            long val = 0;
            if (Int64.TryParse(data[Index], out val))
            {
                long comp = 0;
                if (Int64.TryParse(FilterValue, out comp))
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
            if (Int64.TryParse(data[Index], out val))
            {
                long comp = 0;
                if (Int64.TryParse(FilterValue, out comp))
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
            if (Int64.TryParse(data[Index], out val))
            {
                long comp = 0;
                if (Int64.TryParse(FilterValue, out comp))
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
            if (Int64.TryParse(data[Index], out val))
            {
                long comp = 0;
                if (Int64.TryParse(FilterValue, out comp))
                {
                    return comp >= val;
                }
                return false;
            }
            return false;
        }
    }
}
