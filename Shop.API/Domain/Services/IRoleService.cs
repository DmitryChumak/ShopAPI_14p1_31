using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services.Communication;

namespace Shop.API.Domain.Services
{
    public interface IRoleService
    {
        Task<RoleResponse> DeleteAsync(int id);
        Task<IEnumerable<Role>> ListAsync();
        Task<RoleResponse> SaveAsync(Role role);
        Task<RoleResponse> UpdateAsync(int id, Role role);
    }
}