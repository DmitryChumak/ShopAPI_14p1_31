using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shop.API.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> ListAsync();
        Task<TEntity> FindByIdAsync(int id);
        Task AddAsync(TEntity entity);
        void Update( TEntity entity);
        void Remove(TEntity entity);
    }
}