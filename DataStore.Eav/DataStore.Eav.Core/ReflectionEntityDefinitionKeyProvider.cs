using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DataStore.Eav.Core
{
    public class ReflectionEntityDefinitionKeyProvider : IEntityDefinitionKeyProvider
    {
        public string GetEntityDefinitionKey(Type entityType)
        {
            if (entityType == null)
                return null;

            var assemblyName = entityType.GetTypeInfo().Assembly.GetName();

            return $"{assemblyName.Name}_{assemblyName.Version}_{entityType.FullName}";
        }
    }
}
