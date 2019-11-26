namespace DataStore.Eav.Core
{
    /// <summary>
    /// Interface of model cache key providers.
    /// </summary>
    /// <typeparam name="T">Thy type of the model class this provider can generate cache keys for.</typeparam>
    public interface IModelCacheKeyProvider<T>
    {
        /// <summary>
        /// Gets the cache key provider of the given model.
        /// </summary>
        /// <param name="model">The model to get cache key of.</param>
        /// <returns>Returns the cache key.</returns>
        string GetCacheKey(T model);
    }
}
