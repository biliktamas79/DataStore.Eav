using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataStore.Eav.Core
{
    /// <summary>
    /// Interface of model definition providers that can provide model definitions for different types.
    /// </summary>
    public interface IModelDefinitionProvider
    {
        /// <summary>
        /// Gets the model definition for the given type, or null if not found.
        /// </summary>
        /// <param name="cancellationToken">The token used for cancelling this operation.</param>
        /// <returns>Returns the model definition if found, or null otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="modelType"/> is null.</exception>
        Task<ModelDefinition> GetModelDefinition(Type modelType, CancellationToken cancellationToken);
    }
}
