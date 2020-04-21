using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services.Communication;
using Shop.API.Persistence.Helpers;

namespace Shop.API.Domain.Services
{
    public interface IProductService
    {
        Task<PagedList<Product>> ListAsync(ProductParams productParams);
        Task<ProductResponse> DeleteAsync(int id);
        Task<ProductResponse> SaveAsync(Product category);
        Task<ProductResponse> UpdateAsync(int id, Product category);
    }
}