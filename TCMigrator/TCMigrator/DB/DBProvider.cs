using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Interfaces;
using sql = TCMigrator.Properties.Database;

namespace TCMigrator.DB
{
    public static class DBProvider
    {
        public static TCDataUtilities.Database.IDbConnection GetDBConnection()
        {
            if (Properties.Database.Default.IsMSSQL)
            {
                return new TCDataUtilities.Database.SQL.SQLConnection(sql.Default.UserName, sql.Default.Password, sql.Default.DatabaseName, sql.Default.SQLServerAddress);
            }
            else
            {
                if (!String.IsNullOrWhiteSpace(sql.Default.TablePrefix))
                {
                    return new TCDataUtilities.Database.Oracle.OracleConnection(sql.Default.OracleUserName, sql.Default.OracleIP, Int32.Parse(sql.Default.OraclePort), sql.Default.OracleServiceName, sql.Default.OraclePassword, "TCP", sql.Default.TablePrefix);
                }
                return new TCDataUtilities.Database.Oracle.OracleConnection(sql.Default.OracleUserName, sql.Default.OracleIP, Int32.Parse(sql.Default.OraclePort), sql.Default.OracleServiceName, sql.Default.OraclePassword);
            }
        }
    }
}
