using Microsoft.EntityFrameworkCore;
using SimpleChat.Data.Contexts.Contracts;
using SimpleChat.Data.Entities;
using SimpleChat.Data.Repositories.Contracts;

namespace SimpleChat.Data.Repositories.Implementation
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected IApplicationDbContext appContext;

        public BaseRepository(IApplicationDbContext appContext)
        {
            this.appContext = appContext;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() =>
           await appContext.Set<TEntity>()
           .AsNoTracking()
           .ToListAsync();

        public virtual async Task<TEntity?> GetByIdAsync(int id) =>
            await appContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var created = await appContext.Set<TEntity>().AddAsync(entity);
            await appContext.SaveChangesAsync();

            return created.Entity;
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            appContext.Set<TEntity>().Update(entity);
            await appContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await appContext.Set<TEntity>().FindAsync(id);

            appContext.Set<TEntity>().Remove(entity);
            await appContext.SaveChangesAsync();
        }
    }
}
