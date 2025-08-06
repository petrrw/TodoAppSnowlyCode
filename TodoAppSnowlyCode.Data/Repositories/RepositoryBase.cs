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
        public async Task<List<TEntity>> GetAllAsync(CancellationToken ct) => await dbSet.ToListAsync(ct);

        /// <inheritdoc/>
        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct)
        {
            var entity = await dbSet.FindAsync(id, ct);

            if (entity is not null)
                dbContext.Entry(entity).State = EntityState.Detached;

            return entity;

        }

        /// <inheritdoc/>
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken ct)
        {
            var inserted = await dbSet.AddAsync(entity, ct);
            await dbContext.SaveChangesAsync(ct);

            return inserted.Entity;
        }

        /// <inheritdoc/>
        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken ct)
        {
            var inserted = dbSet.Update(entity);
            await dbContext.SaveChangesAsync(ct);

            return inserted.Entity;
        }

        /// <inheritdoc/>
        public async Task RemoveAsync(int id, CancellationToken ct)
        {
            TEntity? entity = await dbSet.FindAsync(id, ct);

            if (entity is null)
                return;

            try
            {
                dbSet.Remove(entity);
                await dbContext.SaveChangesAsync(ct);
            }
            catch
            {
                dbContext.Entry(entity).State = EntityState.Unchanged;
                throw;
            }
        }
    }
}
