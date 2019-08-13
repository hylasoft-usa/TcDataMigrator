using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Data;
using TCMigrator.Teamcenter;

namespace TCMigrator.Interfaces
{
    public interface IPageMediator
    {
        void advance();
        void retreat();
        void updateData(ImportData data);
        ImportData getCurrentData();
        void updateImportOptions(CSVConverterOptions o);
        CSVConverterOptions getCurrentImportOptions();
    }
}
