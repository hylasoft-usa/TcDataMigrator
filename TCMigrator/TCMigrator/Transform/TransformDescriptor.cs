using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCMigrator.Enums;

namespace TCMigrator.Transform
{
    public class TransformDescriptor
    {
        public int id { get; set; }
        public TransformType type { get; set; }
        public String value { get; set; }
        public String replacement { get; set; }
    }
}
