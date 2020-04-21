using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.API.Domain.Models;
using Shop.API.Domain.Repositories;
using Shop.API.Persistence.Contexts;
using Shop.API.Persistence.Helpers;

namespace Shop.API.Persistence.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public new async Task<PagedList<Product>> ListAsync(ProductParams productParams)
        {
            var query = context.Products.Include(p=>p.Category).OrderByDescending(p => p.Name).AsQueryable();

            if (!string.IsNullOrEmpty(productParams.OrderBy))
            {
                switch (productParams.OrderBy)
                {
                    case "low":
                        query = query.OrderByDescending(p => p.Name);
                        break;
                    default:
                        query = query.OrderBy(p => p.Name);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(productParams.CategoryName))
                query = query.Where(p=>p.Category.Name == productParams.CategoryName);

            return await PagedList<Product>.CreateAsync(query, productParams.PageNumber, productParams.PageSize);
        }

       
    }
}