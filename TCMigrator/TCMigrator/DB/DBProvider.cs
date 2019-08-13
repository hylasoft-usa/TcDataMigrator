using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Interfaces;

namespace TCMigrator.DB
{
    public static class DBProvider
    {
        public static IDbConnection GetDBConnection()
        {
            if (Properties.Database.Default.IsMSSQL)
            {
                return new SQLConnection();
            }
            else
            {
                return new OracleConnection();
            }
        }
    }
}
