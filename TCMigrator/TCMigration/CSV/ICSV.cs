using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCDataUtilities.DataModel;

namespace TCDataUtilities.CSV
{
    public interface ICSV
    {
        string Write(ImportData data, string writeLocation);
        void SetSeparator(Char sep);
    }
}
