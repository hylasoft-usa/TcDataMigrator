using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Data;
using TCMigrator.Interfaces;
using ODC=Oracle.ManagedDataAccess.Client;

namespace TCMigrator.DB
{
    class OracleConnection : IDbConnection
    {
        private Dictionary<String, String> mapping;
        private List<OracleColumnMap> columnMap;
        public OracleConnection()
        {
            this.mapping = getMappings();
            this.columnMap = getOracleColumnMap();
        }
        public Dictionary<String,String> getMappings()
        {
            Dictionary<String, String> mappings = new Dictionary<string, string>();
            using (var con = getConnection())
            {
                var command = con.CreateCommand();
                command.CommandText = "SELECT * From EUSA_PREFIXJUNCTION";
                command.Connection = con;
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mappings.Add(reader.GetString(0), reader.GetString(1));
                    }
                }
                con.Close();
            }
            return mappings;
        }
        public List<OracleColumnMap> getOracleColumnMap()
        {
            List<OracleColumnMap> map = new List<OracleColumnMap>();
            using(var con = getConnection())
            {
                var com = con.CreateCommand();
                com.CommandText = "SELECT * from EUSA_COLUMNTOHEADER";
                con.Open();
                using(var reader = com.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var objectType = reader.GetString(0);
                        var columnName = reader.GetString(1);
                        var header = reader.GetString(2);
                        map.Add(new OracleColumnMap(objectType, header, columnName)); ;
                    }
                }
            }
            return map;
        }
        public List<string> AutogenerateHeaderRow(string tableName)
        {
            var columns = getTableColumns(tableName);
            return AutogenerateHeaderRow(columns);
        }

        public List<string> AutogenerateHeaderRow(List<string> columnNames)
        {
            var headers = new List<String>();
            foreach (String s in columnNames)
            {
                if (s.Contains("__"))
                {
                    var parts = s.Split(new[] { "__" }, StringSplitOptions.RemoveEmptyEntries);
                    var itemType = getItemType(parts[0]);
                    if (!String.IsNullOrWhiteSpace(itemType))
                    {
                        headers.Add(String.Format("{0}:{1}", itemType, getHeaderValue(itemType, parts[1])));
                    }
                    else { headers.Add(s); }
                }
                else { headers.Add(s); }
            }
            return headers;
        }
        public string getItemType(string typeCode)
        {
            var tc = typeCode.ToLower();
            if (mapping.ContainsKey(tc))
            {
                return mapping[tc];
            }
            return "";
        }
        public string getHeaderValue(string itemType, string headerValue)
        {
            var filter = columnMap.Where(x => x.ObjectType.ToLower() == itemType.ToLower());
            var f2 = filter.Where(x => x.ColumnName.ToLower() == headerValue.ToLower()).FirstOrDefault();
            if (f2 != null) { return f2.HeaderText; }
            else { return ""; }
        }

        public List<string[]> getEntries(string tableName)
        {
            List<String[]> entries = new List<String[]>();
            var con = getConnection();
            var command = con.CreateCommand();
            command.CommandText = "SELECT * from " + tableName;
            con.Open();
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var ColCount = reader.FieldCount;
                    var arr = new List<String>();
                    for (var x = 0; x < ColCount; x++)
                    {
                        arr.Add(reader.GetString(x));
                    }
                    entries.Add(arr.ToArray());
                }
            }
            con.Close();
            return entries;
        }

        public List<string[]> getEntries(string tablename, List<string> columns)
        {
            List<String[]> entries = new List<String[]>();
            var con = getConnection();
            var command = con.CreateCommand();
            command.CommandText = "SELECT";
            foreach(string s in columns)
            {
                command.CommandText += " " + s + ", ";
            }
            command.CommandText = command.CommandText.Substring(0, command.CommandText.Length - 2)+ " ";
            command.CommandText += "from " + tablename;
            con.Open();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var ColCount = reader.FieldCount;
                    var arr = new List<String>();
                    for (var x = 0; x < ColCount; x++)
                    {
                        arr.Add(reader.GetValue(x).ToString());
                    }
                    entries.Add(arr.ToArray());
                }
            }
            con.Close();
            return entries;
        }

        public List<string> getTableColumns(string tableName)
        {
            List<String> columns = new List<String>();
            var con = getConnection();
            var command = con.CreateCommand();
            command.CommandText = "SELECT column_name FROM DBA_TAB_COLS WHERE table_name = '" + tableName + "'";
            con.Open();
            using(var reader= command.ExecuteReader())
            {
                while (reader.Read())
                {
                    columns.Add(reader.GetString(0));
                }
            }
            con.Close();
            return columns;

        }

        public List<string> getTables()
        {
            List<String> tables = new List<String>();
            var con = getConnection();
            var command = con.CreateCommand();
            if (!String.IsNullOrWhiteSpace(Properties.Database.Default.TablePrefix))
            {
                command.CommandText = "SELECT table_name FROM all_tables where table_name like '%" + Properties.Database.Default.TablePrefix + "%'";
            }
            else
            {
                command.CommandText = "SELECT table_name FROM all_tables";
            }
            con.Open();
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    tables.Add(reader.GetString(0));
                }
            }
            con.Close();
            return tables;

        }
        public ODC.OracleConnection getConnection()
        {
            var con = new ODC.OracleConnection();
            con.ConnectionString = String.Format(Properties.Database.Default.OracleConnectionString, Properties.Database.Default.OracleIP, Properties.Database.Default.OraclePort, Properties.Database.Default.OracleServiceName, Properties.Database.Default.OracleUserName, Properties.Database.Default.OraclePassword);
            return con;
        }

        public bool testConnection()
        {
            var con = new ODC.OracleConnection();
            con.ConnectionString = String.Format(Properties.Database.Default.OracleConnectionString, Properties.Database.Default.OracleIP, Properties.Database.Default.OraclePort, Properties.Database.Default.OracleServiceName, Properties.Database.Default.OracleUserName, Properties.Database.Default.OracleSchema, Properties.Database.Default.OraclePassword);
            try
            {
                con.Open();
                con.Close();
                return true;
            }catch(Exception e)
            {
                //log Error
                return false;
            }
        }
    }
}
