using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Eav.Core
{
    [Flags]
    public enum AttributeFlagsEnum : int
    {
        None = 0,
        Nullable = 1,
        Collection = 2,
        Ordered = 4,
    }
}
