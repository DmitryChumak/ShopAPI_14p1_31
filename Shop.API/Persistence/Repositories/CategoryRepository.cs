using Shop.API.Persistence.Contexts;
using Shop.API.Domain.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shop.API.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Shop.API.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

    }
}