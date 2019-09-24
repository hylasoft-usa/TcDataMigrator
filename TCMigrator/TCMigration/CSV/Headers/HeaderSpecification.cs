using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCDataUtilities.CSV.Headers
{
    public class HeaderSpecification
    {
        private string _columnName;
        private string _itemType;
        private string _headerValue;
        private int _headerIndex;
        bool isSimple;
        public string ColumnName { get { return _columnName; } }
        public string ItemType { get { return _itemType; } }
        public string HeaderText { get { return _headerValue; } }
        public int Index { get { return _headerIndex; } }
        public HeaderSpecification(string columnName,string itemType, string headerValue,int headerIndex = -1)
        {
            _columnName = columnName;
            _itemType = ItemType;
            _headerValue = headerValue;
            _headerIndex = headerIndex;
        }
        public HeaderSpecification(string columnName, string headerValue)
        {
            isSimple = true;
            _columnName = columnName;
            _headerValue = headerValue;
        }
    }
}
