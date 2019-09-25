using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCDataUtilities.DataModel;
using TCMigrator.Transform;

namespace TCMigrator.Interfaces
{
    public interface ITransformer
    {
        ImportData transform(ImportData data, TransformOptions options, Action<string> updateFunction = null);
        ImportData replace(ImportData data, TransformOptions options, Action<string> updatefunction = null);
        ImportData remove(ImportData data, TransformOptions options, Action<string> callback = null);
        ImportData trim(ImportData data,TransformOptions options);
    }
}
