using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DataStore.Eav.Core
{
    public class AttributeDefinition
    {
        public readonly string Name;
        public readonly AttributeTypeEnum Type;
        public readonly AttributeFlagsEnum Flags;

        public AttributeDefinition(string name, AttributeTypeEnum type, AttributeFlagsEnum flags)
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Type = type;
            this.Flags = flags;
        }

        public bool IsNullable
        {
            get { return this.Flags.HasFlag(AttributeFlagsEnum.Nullable); }
        }

        public bool IsCollection
        {
            get { return this.Flags.HasFlag(AttributeFlagsEnum.Collection); }
        }

        public bool IsOrdered
        {
            get { return this.Flags.HasFlag(AttributeFlagsEnum.Ordered); }
        }
    }
}
