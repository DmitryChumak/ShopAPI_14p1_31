using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.API.Persistence.Helpers;

namespace Shop.API.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListAsync();
        Task<PagedList<TEntity>> ListAsync(ProductParams productParams);
        Task<TEntity> FindByIdAsync(int id);
        Task AddAsync(TEntity entity);
        void Update( TEntity entity);
        void Remove(TEntity entity);
    }
}