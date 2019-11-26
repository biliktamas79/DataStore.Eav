using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Eav.Core
{
    [Flags]
    public enum AttributeTypeEnum : int
    {
        Boolean = 1,
        Char = 2,
        String = 4,
        Byte = 8,
        Int16 = 16,
        Int32 = 32,
        Int64 = 64,
        SByte = 128,
        UInt16 = 256,
        UInt32 = 512,
        UInt64 = 1024,
        Float = 2048,
        Double = 4096,
        Decimal = 8192,
        Guid = 16384,
    }
}
