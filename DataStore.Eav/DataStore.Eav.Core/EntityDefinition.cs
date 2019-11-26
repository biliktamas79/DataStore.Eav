using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Eav.Core
{
    public class EntityDefinition
    {
        public readonly string Key;
        
        /// <summary>
        /// The attributes of 
        /// </summary>
        public readonly List<AttributeDefinition> Attributes;

        public EntityDefinition(string key)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.Attributes = new List<AttributeDefinition>();
        }
    }
}
