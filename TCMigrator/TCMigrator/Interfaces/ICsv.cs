using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCDataUtilities.DataModel;

namespace TCMigrator.Interfaces
{
    public interface ICsv
    {
        string Write(ImportData data);
        void WriteNoChange(ImportData data);
        void SetSeparator(Char sep);
    }
}
