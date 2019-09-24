using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCDataUtilities.Filter
{
    public enum FilterType
    {
        LESS_THAN,
        LESS_THAN_OR_EQUAL_TO,
        EQUAL_TO,
        GREATER_THAN_OR_EQUAL_TO,
        GREATER_THAN,
        BEFORE,
        AFTER,
        STARTS_WITH,
        ENDS_WITH,
        CONTAINS,
        IS_EMPTY,
        IS_NULL

    }
}
