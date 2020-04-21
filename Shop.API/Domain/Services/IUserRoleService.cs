using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services.Communication;

namespace Shop.API.Domain.Services
{
    public interface IUserRoleService
    {
        Task<UserResponse> DeleteRoleAsync(int userId, int roleId);
        Task<IEnumerable<User>> ListUsersByRoleAsync(int roleId);
        Task<UserResponse> SetUserRoleAsync(int userId, int roleId);
    }
}