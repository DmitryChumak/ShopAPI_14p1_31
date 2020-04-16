using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services.Communication;

namespace Shop.API.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<ProductResponse> DeleteAsync(int id);
        Task<ProductResponse> SaveAsync(Product category);
        Task<ProductResponse> UpdateAsync(int id, Product category);
    }
}