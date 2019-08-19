using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Interfaces;
using ODC=Oracle.ManagedDataAccess.Client;

namespace TCMigrator.DB
{
    class OracleConnection : IDbConnection
    {
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
        public List<string> AutogenerateHeaderRow(string tableName)
        {
            var mappings = getMappings();
            var columns = getTableColumns(tableName);
            var headers = new List<String>();
            foreach(String s in columns)
            {
                var parts = s.Split(new[] { "__" }, StringSplitOptions.RemoveEmptyEntries);
                if (mappings.ContainsKey(parts[0]))
                {
                    headers.Add(mappings[parts[0]] + ":" + parts[1]);
                }
            }
            return headers;
        }

        public List<string> AutogenerateHeaderRow(List<string> columnNames)
        {
            var mappings = getMappings();
            var headers = new List<String>();
            foreach (String s in columnNames)
            {
                var parts = s.Split(new[] { "__" }, StringSplitOptions.RemoveEmptyEntries);
                if (mappings.ContainsKey(parts[0].ToLower()))
                {
                    headers.Add(mappings[parts[0].ToLower()] + ":" + parts[1]);
                }
            }
            return headers;
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
                var ColCount = reader.FieldCount;
                var arr = new List<String>();
                for (var x = 0; x < ColCount; x++)
                {
                    arr.Add(reader.GetString(x));
                }
                entries.Add(arr.ToArray());
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
                reader.Read();
                var ColCount = reader.FieldCount;
                var arr = new List<String>();
                for (var x = 0; x < ColCount; x++)
                {
                    arr.Add(reader.GetValue(x).ToString());
                }
                entries.Add(arr.ToArray());
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
