using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Interfaces;
using IDbConnection = TCMigrator.Interfaces.IDbConnection;

namespace TCMigrator.DB
{
    public class SQLConnection : IDbConnection
    {
        public List<string> AutogenerateHeaderRow(string tableName)
        {
            List<String> cols = getTableColumns(tableName);
            Dictionary<String, String> mappings = new Dictionary<string, string>();
            using (SqlConnection con = _connect())
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * From EUSA_PrefixJunction";
                command.Connection = con;
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mappings.Add(reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            List<String> headers = new List<String>();
            foreach (String s in cols)
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
            Dictionary<String, String> mappings = new Dictionary<string, string>();
            using (SqlConnection con = _connect())
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT * From EUSA_PrefixJunction";
                command.Connection = con;
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mappings.Add(reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            List<String> headers = new List<String>();
            foreach (String s in columnNames)
            {
                var parts = s.Split(new[] { "__" }, StringSplitOptions.RemoveEmptyEntries);
                if (mappings.ContainsKey(parts[0]))
                {
                    headers.Add(mappings[parts[0]] + ":" + parts[1]);
                }
                else
                {
                    //add column name directly
                    headers.Add(s);
                }
            }
            return headers;
        }

        public List<string[]> getEntries(string tableName)
        {
            using (SqlConnection con = _connect())
            {
                List<String[]> entries = new List<String[]>();
                var command = new SqlCommand();
                command.CommandText = "SELECT * from " + tableName;
                command.Connection = con;
                con.Open();
                using (var reader = command.ExecuteReader())
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
                return entries;
            }
        }

        public List<string[]> getEntries(string tablename, List<string> columns)
        {
            using (SqlConnection con = _connect())
            {
                List<String[]> entries = new List<String[]>();
                var command = new SqlCommand();
                command.CommandText = "SELECT ";
                foreach (String s in columns)
                {
                    command.CommandText += s + ", ";
                }
                command.CommandText = command.CommandText.Substring(0, command.CommandText.Length - 2);
                command.CommandText += " from " + tablename;
                command.Connection = con;
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
                return entries;
            }
        }

        public List<string> getTableColumns(string tableName)
        {
            using (SqlConnection con = _connect())
            {
                string[] restrictions = new string[4] { null, null, tableName, null };
                con.Open();
                return con.GetSchema("Columns", restrictions).AsEnumerable().Select(s => s.Field<String>("Column_Name")).ToList();
            }
        }

        public List<string> getTables()
        {
            using (SqlConnection con = _connect())
            {
                List<String> tables = new List<String>();
                con.Open();
                DataTable dt = con.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    if (((string)row[2]).Contains("EUSA_"))
                    {
                        tables.Add((string)row[2]);
                    }
                }
                return tables;
            }
        }

        public bool testConnection()
        {
            using (SqlConnection con = _connect())
            {
                try
                {
                    con.Open();
                    return true;
                }
                catch (Exception e)
                {
                    //log error
                    return false;
                }
            }
        }
        private SqlConnection _connect()
        {
            var dbProps = Properties.Database.Default;
            if (Properties.Database.Default.UseWindowsAuth)
            {
                return new SqlConnection(String.Format(dbProps.SQLServerIntegratedConString, dbProps.SQLServerAddress, dbProps.DatabaseName));
            }
            return new SqlConnection(String.Format(dbProps.SQLServerSqlAuth, dbProps.SQLServerAddress, dbProps.DatabaseName, dbProps.UserName, dbProps.Password));
        }
    }
}
