namespace TodoAppSnowlyCode.Data.Interfaces
{
    /// <summary>
    /// Base interface for all repositories implementing basic CRUD operations.
    /// </summary>
    /// <typeparam name="TEntity">Entity the repository works with</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Returns all records
        /// </summary>
        /// <returns>List of all found records</returns>
        List<TEntity> GetAll();

        /// <summary>
        /// Finds an entity by id
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <returns>Found entity, otherwise null</returns>
        TEntity? FindById(int id);

        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        void Add(TEntity entity);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="id"></param>
        void Remove(int id);
    }
}
