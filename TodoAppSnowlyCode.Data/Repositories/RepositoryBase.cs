using Microsoft.EntityFrameworkCore;
using TodoAppSnowlyCode.Data.DbSetup;
using TodoAppSnowlyCode.Data.Interfaces;

namespace TodoAppSnowlyCode.Data.Repositories
{
    /// <summary>
    /// Base respository class providing basic CRUD operations for specific repositories
    /// </summary>
    /// <typeparam name="TEntity">Entity for underyling <see cref="DbSet{TEntity}"/></typeparam>
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Underlying DBContext for this repository
        /// </summary>
        protected AppDbContext dbContext;

        /// <summary>
        /// Underlying <see cref="DbSet{TEntity}"/> of dbContext for this repository
        /// </summary>
        protected DbSet<TEntity> dbSet;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Underlying DBContext for the repository</param>
        public RepositoryBase(AppDbContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<TEntity>();
        }

        /// <inheritdoc/>
        public List<TEntity> GetAll() => dbSet.ToList();

        /// <inheritdoc/>
        public TEntity? FindById(int id) => dbSet.Find(id);

        /// <inheritdoc/>
        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
            dbContext.SaveChanges();
        }

        /// <inheritdoc/>
        public void Remove(int id)
        {
            TEntity? entity = dbSet.Find(id);

            if (entity is null)
                return;

            try
            {
                dbSet.Remove(entity);
                dbContext.SaveChanges();
            }
            catch
            {
                dbContext.Entry(entity).State = EntityState.Unchanged;
                throw;
            }
        }
    }
}
