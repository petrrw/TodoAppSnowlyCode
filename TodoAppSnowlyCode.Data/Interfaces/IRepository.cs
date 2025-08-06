namespace TodoAppSnowlyCode.Data.Interfaces
{
    /// <summary>
    /// Base interface for all repositories implementing basic CRUD operations.
    /// </summary>
    /// <typeparam name="TEntity">Entity the repository works with</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Retrieves all entities from the data store.
        /// </summary>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>List of all entities.</returns>
        Task<List<TEntity>> GetAllAsync(CancellationToken ct);

        /// <summary>
        /// Finds an entity by id.
        /// </summary>
        /// <param name="id">Entity ID.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Found entity if exists, otherwise null.</returns>
        Task<TEntity?> GetByIdAsync(int id, CancellationToken ct);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>The added entity.</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="entity">Updated entity.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Task</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct);

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="id">ID of the item to remove.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Task</returns>
        Task RemoveAsync(int id, CancellationToken ct);
    }
}
