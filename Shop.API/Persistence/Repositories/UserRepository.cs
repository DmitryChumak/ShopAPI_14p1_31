using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shop.API.Domain.Models;
using Shop.API.Domain.Repositories;
using Shop.API.Persistence.Contexts;

namespace Shop.API.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }


        public new async Task<User> FindByIdAsync(int id)
        {
            return await context.Users
                                    .Include(x => x.UserRoles)
                                        .ThenInclude(x => x.Role)
                                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public new async Task<IEnumerable<User>> ListAsync()
        {
            return await context.Users.Include(x => x.UserRoles)
                                        .ThenInclude(x => x.Role)
                                            .ToListAsync();
        }

    }
}