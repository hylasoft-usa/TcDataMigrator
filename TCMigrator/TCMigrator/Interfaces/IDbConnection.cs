using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCMigrator.Interfaces
{
    public interface IDbConnection
    {
        bool testConnection();
        List<String> getTables();
        List<String> getTableColumns(String tableName);
        List<String[]> getEntries(String tableName);
        List<String[]> getEntries(String tablename, List<String> columns);
        List<String> AutogenerateHeaderRow(String tableName);
        List<String> AutogenerateHeaderRow(List<String> columnNames);
    } 
}
