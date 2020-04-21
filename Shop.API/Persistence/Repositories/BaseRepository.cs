using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.API.Domain.Repositories;
using Shop.API.Persistence.Contexts;
using Shop.API.Persistence.Helpers;

namespace Shop.API.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity :class 
    {
        protected readonly AppDbContext context;
        DbSet<TEntity> dbSet;
        public BaseRepository(AppDbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>>  ListAsync()
        {
            return await dbSet.ToListAsync();
        }

        public void Update(TEntity entity)
        {
            dbSet.Update(entity);
        }

        public Task<PagedList<TEntity>> ListAsync(ProductParams productParams)
        {
            throw new System.NotImplementedException();
        }
    }
}