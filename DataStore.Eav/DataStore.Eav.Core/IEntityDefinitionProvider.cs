using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataStore.Eav.Core
{
    /// <summary>
    /// Interface of entity definition providers that can provide entity definitions for different types.
    /// </summary>
    public interface IEntityDefinitionProvider
    {
        /// <summary>
        /// Gets the entity definition for the given type, or null if not found.
        /// </summary>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the entity definition if found, or null otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="entityType"/> is null.</exception>
        Task<EntityDefinition> GetEntityDefinition(Type entityType, CancellationToken cancellationToken);
    }
}
