using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.Data
{
    public class OracleColumnMap
    {
        public string ObjectType;
        public string HeaderText;
        public string ColumnName;

        public OracleColumnMap(string type, string text, string colName)
        {
            this.ObjectType = type;
            this.HeaderText = text;
            this.ColumnName = colName;
        }
    }
}
