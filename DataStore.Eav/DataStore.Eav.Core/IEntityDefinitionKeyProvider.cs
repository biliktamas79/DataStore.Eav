using System;

namespace DataStore.Eav.Core
{
    /// <summary>
    /// Interface of entity definition key providers.
    /// </summary>
    public interface IEntityDefinitionKeyProvider
    {
        /// <summary>
        /// Gets the entity definition key of the given type.
        /// </summary>
        /// <param name="entityType">The entity type to get definition key of.</param>
        /// <returns>Returns the entity definition key.</returns>
        string GetEntityDefinitionKey(Type entityType);
    }
}
