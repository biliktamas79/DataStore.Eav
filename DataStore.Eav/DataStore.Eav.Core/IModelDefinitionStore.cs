using System.Threading;
using System.Threading.Tasks;

namespace DataStore.Eav.Core
{
    /// <summary>
    /// Interface of model definition stores.
    /// </summary>
    public interface IModelDefinitionStore
    {
        /// <summary>
        /// Checks whether a model definition with the given key exists.
        /// </summary>
        /// <param name="key">The key of the model definition to check.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns true if the model definition is found, or false if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is null.</exception>
        Task<bool> ExistsModelDefinition(string key, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the model definition with the given key, or null if not found.
        /// </summary>
        /// <param name="key">The key of the model definition to get.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the model definition if found, or null if not found.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="key"/> is null.</exception>
        Task<ModelDefinition> GetModelDefinition(string key, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the model definition keys.
        /// </summary>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the model definition keys in this provider.</returns>
        Task<string[]> GetModelDefinitionKeys(CancellationToken cancellationToken);

        /// <summary>
        /// Adds the given model definition to this store, or throws an exception if a model definition with the same key already exists.
        /// </summary>
        /// <param name="modelDefinition">The model definition to add.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        Task AddModelDefinitionOrThrow(ModelDefinition modelDefinition, CancellationToken cancellationToken);

        /// <summary>
        /// Sets the given model definition to this store by updating if already exists with the same key, or adding if not.
        /// </summary>
        /// <param name="modelDefinition">The model definition to set.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        Task SetModelDefinition(ModelDefinition modelDefinition, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the model definition with the given key from this store.
        /// </summary>
        /// <param name="modelDefinitionKey">The key of the model definition to remove.</param>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns true if the model definition existed and was successfully removed, otherwise returns false.</returns>
        Task<bool> RemoveModelDefinition(string modelDefinitionKey, CancellationToken cancellationToken);
    }
}
