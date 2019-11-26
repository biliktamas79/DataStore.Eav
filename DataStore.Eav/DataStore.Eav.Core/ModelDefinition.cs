using System;
using System.Collections.Generic;
using System.Text;

namespace DataStore.Eav.Core
{
    public class ModelDefinition
    {
        public readonly string Key;
        public readonly Dictionary<string, EntityDefinition> EntityDefinitionsByName;

        public ModelDefinition(string key)
        {
            this.Key = key ?? throw new ArgumentNullException(nameof(key));
            this.EntityDefinitionsByName = new Dictionary<string, EntityDefinition>();
        }
    }
}
