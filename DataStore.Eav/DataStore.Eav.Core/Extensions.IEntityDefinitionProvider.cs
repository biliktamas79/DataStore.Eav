using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataStore.Eav.Core
{
    public static partial class Extensions
    {
        /// <summary>
        /// Gets the entity definition for the given type from the provider, or throws if not found.
        /// (Can be used for ensuring no null is returned.)
        /// </summary>
        /// <param name="provider">The provider to get entity definitions from.</param>
        /// <param name="entityType">The type of the entity to get definition for.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the non-null entity definition if found, or throws an exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="provider"/> or <paramref name="entityType"/> is null.</exception>
        /// <exception cref="NullReferenceException">Thrown if <paramref name="provider"/> returns null.</exception>
        public static async Task<EntityDefinition> GetEntityDefinitionOrThrow(this IEntityDefinitionProvider provider, Type entityType, CancellationToken cancellationToken)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            var entityDefinition = await provider.GetEntityDefinition(entityType, cancellationToken).ConfigureAwait(false);
            if (entityDefinition == null)
                throw new NullReferenceException($"Entity definition provider of type '{provider.GetType().FullName}' could not get entity for type '{entityType.FullName}'.");

            return entityDefinition;
        }

        /// <summary>
        /// Gets the entity definition for the given type from the provider, or throws if not found.
        /// </summary>
        /// <param name="provider">The provider to get entity definitions from.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the non-null entity definition if found, or throws an exception.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="provider"/> is null.</exception>
        /// <exception cref="NullReferenceException">Thrown if <paramref name="provider"/> returns null.</exception>
        public static Task<EntityDefinition> GetEntityDefinitionOrThrow<Tentity>(this IEntityDefinitionProvider provider, CancellationToken cancellationToken)
        {
            return GetEntityDefinitionOrThrow(provider, typeof(Tentity), cancellationToken);
        }

        /// <summary>
        /// Gets the entity definition for the given type, or null if not found.
        /// </summary>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the entity definition if found, or null otherwise.</returns>
        public static Task<EntityDefinition> GetEntityDefinition<Tentity>(this IEntityDefinitionProvider provider, CancellationToken cancellationToken)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            return provider.GetEntityDefinition(typeof(Tentity), cancellationToken);
        }
    }
}
