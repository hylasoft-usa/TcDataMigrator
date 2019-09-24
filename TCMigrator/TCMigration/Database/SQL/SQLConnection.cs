using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Statements = TCMigration.MSSQLStatements;

namespace TCDataUtilities.Database.SQL
{
    public class SQLConnection : IDbConnection
    {
        private string User;
        private string Password;
        private string Database;
        private string Address;
        private readonly string WindowsAuthString = "Server ={0}; Initial Catalog = { 1 }; Integrated Security = true;";
        private readonly string SQLServerAuthString = "Server ={0}; Initial Catalog = { 1 }; User Id = { 2 }; Password={3};";
        private bool useWindowsAuth;
        private readonly string ORDER_BY = "CREATION_DATE";
        private string TablePrefix;

        public SQLConnection(string user, string password, string dbName, string address,string tablePrefix="")
        {
            User = user;
            Password = password;
            Database = dbName;
            Address = address;
            useWindowsAuth = false;
            this.TablePrefix = tablePrefix;
        }
        public SQLConnection(string dbName, string address,string tablePrefix="")
        {
            Address = address;
            Database = dbName;
            useWindowsAuth = true;
            this.TablePrefix = tablePrefix;
        }

public List<string> AutogenerateHeaderRow(string tableName)
        {
            List<String> cols = getTableColumns(tableName);
            var mappings = getMappings();
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
        public Dictionary<string,string> getMappings()
        {
            Dictionary<string,string> mappings = new Dictionary<string, string>();
            using (SqlConnection con = _connect())
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = String.IsNullOrWhiteSpace(TablePrefix) ? Statements.GetMappings : String.Format(Statements.GetMappingsWithPrefix, TablePrefix);
                con.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mappings.Add(reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            return mappings;
        }

        public List<string> AutogenerateHeaderRow(List<string> columnNames)
        {
            var mappings = getMappings();
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
                    headers.Add(s);
                }
            }
            return headers;
        }
        public string getCaseSensitiveHeaderValue(string itemType, string headerValue)
        {
            //Sql Column names are perfectly case sensitive as is
            return headerValue;
        }

        public List<string[]> getEntries(string tableName)
        {
            using (SqlConnection con = _connect())
            {
                List<String[]> entries = new List<String[]>();
                var command = new SqlCommand();
                command.CommandText = orderBy(tableName, String.Format(Statements.SelectAllData, tableName));
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
                var colList = formatColumnList(columns);
                command.CommandText = orderBy(columns, String.Format(Statements.SelectSpecificColumns, colList, tablename));
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
        private string orderBy(string tableName, string command)
        {
            var cols = getTableColumns(tableName);
            return orderBy(cols, command);
        }
        private string orderBy(List<string> colList, string command)
        {
            bool shouldOrder = false;
            foreach(string s in colList)
            {
                if (s.ToLower() == ORDER_BY.ToLower())
                {
                    shouldOrder = true;
                }
            }
            if (shouldOrder)
            {
                command += " ";
                command += String.Format(Statements.OrderBy, ORDER_BY);
            }
            return command;
        }
        private string formatColumnList(List<string> columns)
        {
            var colList = "";
            foreach(string s in columns)
            {
                colList += String.Format("{0},",s);
            }
            return colList.Substring(0, colList.Length - 2);
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
                    tables.Add((string)row[2]);
                }
                return tables;
            }
        }

        private SqlConnection _connect()
        {
            if (useWindowsAuth)
            {
                return new SqlConnection(String.Format(WindowsAuthString, Address, Database));
            }
            return new SqlConnection(String.Format(SQLServerAuthString, Address, Database, User, Password));
        }
    }
}
