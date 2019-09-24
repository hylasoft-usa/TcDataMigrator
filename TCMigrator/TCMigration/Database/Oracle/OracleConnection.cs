using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODC=Oracle.ManagedDataAccess.Client;
using Statements = TCMigration.OracleStatements;

namespace TCDataUtilities.Database.Oracle
{
    public class OracleConnection : IDbConnection
    {
        private Dictionary<String, String> mapping;
        private List<OracleColumnMap> columnMap;
        private readonly string OracleConnectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST = {0})(PORT = {1})))(CONNECT_DATA =(SERVICE_NAME = {2})));User ID = { 3 }; Password={4};";
        private string OracleUser;
        private string OracleIP;
        private string OracleService;
        private string OraclePassword;
        private string OracleProtocol;
        private int OraclePort;
        private readonly string ORDER_COLUMN = "CREATION_DATE";
        string TablePrefix;
        public OracleConnection(string user, string ip,int port, string service, string password, string protocol="TCP",string tablePrefix="")
        {
            this.OracleUser = user;
            this.OracleIP = ip;
            this.OracleService = service;
            this.OraclePassword = password;
            this.OracleProtocol = protocol;
            this.OraclePort = port;
            this.TablePrefix = tablePrefix;
            this.mapping = getMappings();
            this.columnMap = getOracleColumnMap();
        }
        public Dictionary<String,String> getMappings()
        {
            Dictionary<String, String> mappings = new Dictionary<string, string>();
            using (var con = getConnection())
            {
                var command = con.CreateCommand();
                command.CommandText = String.IsNullOrWhiteSpace(TablePrefix)?Statements.GetColumnPrefix:String.Format(Statements.GetColumnPrefixWithPrefix,TablePrefix);
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
                com.CommandText = String.IsNullOrWhiteSpace(TablePrefix) ? Statements.GetColumnMap : String.Format(Statements.GetColumnMapWithPrefix, TablePrefix);
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
                        headers.Add(String.Format("{0}:{1}", itemType, getCaseSensitiveHeaderValue(itemType, parts[1])));
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
        public string getCaseSensitiveHeaderValue(string itemType, string headerValue)
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
            command.CommandText = String.Format(Statements.SelectAllData,tableName);
            if (ShouldOrder(tableName))
            {
                command.CommandText += String.Format(Statements.OrderByAscending, ORDER_COLUMN);
            }
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
            command.CommandText = String.Format(Statements.SelectSpecificColumns, getColumnList(columns), tablename);
            if (ShouldOrder(columns))
            {
                command.CommandText += String.Format(Statements.OrderByAscending, ORDER_COLUMN);
            }
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
        private bool ShouldOrder(string tableName)
        {
            var columns = getTableColumns(tableName);
            return columns.Contains(ORDER_COLUMN);
        }
        private bool ShouldOrder(List<string> selectedCols)
        {
            return selectedCols.Contains(ORDER_COLUMN);
        }
        private string getColumnList(List<string> columns)
        {
            var columnList = "";
            foreach (string s in columns)
            {
                columnList += " " + s + ", ";
            }
            return columnList.Substring(0, columnList.Length - 2);
        }

        public List<string> getTableColumns(string tableName)
        {
            List<String> columns = new List<String>();
            var con = getConnection();
            var command = con.CreateCommand();
            command.CommandText = String.Format(Statements.GetTableColumns,tableName);
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
            using (var con = getConnection())
            {
                var command = con.CreateCommand();
                command.CommandText = String.IsNullOrWhiteSpace(TablePrefix) ? TCMigration.OracleStatements.GetTables : String.Format(TCMigration.OracleStatements.GetTablesWithPrefix, TablePrefix);
                con.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tables.Add(reader.GetString(0));
                    }
                }
                con.Close();
                return tables;
            }
        }
        public ODC.OracleConnection getConnection()
        {
            var con = new ODC.OracleConnection();
            con.ConnectionString = String.Format(OracleConnectionString,OracleIP, OraclePort, OracleService, OracleUser, OraclePassword);
            return con;
        }
    }
}
