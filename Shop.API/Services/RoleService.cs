using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shop.API.Domain.Models;
using Shop.API.Domain.Repositories;
using Shop.API.Domain.Services;
using Shop.API.Domain.Services.Communication;

namespace Shop.API.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUnitOfWork unitOfWork;
        public RoleService(IRoleRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this.roleRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork => unitOfWork;

        public async Task<RoleResponse> DeleteAsync(int id)
        {
            var existingRole = await roleRepository.FindByIdAsync(id);
            if (existingRole == null)
                return new RoleResponse("Role not found");

            try
            {
                roleRepository.Remove(existingRole);
                await UnitOfWork.CompleteAsync();

                return new RoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Error when deleting role: {ex.Message}");
            }

        }

        public async Task<IEnumerable<Role>> ListAsync()
        {
            var roles = await roleRepository.ListAsync();

            return roles.Where(x => x.Name != "SuperAdmin").ToList();
        }

        public async Task<RoleResponse> SaveAsync(Role role)
        {
            try 
            {
                await roleRepository.AddAsync(role);
                await UnitOfWork.CompleteAsync();

                return new RoleResponse(role);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Error occured when saving role: {ex.Message}");
            }
        }

        public async Task<RoleResponse> UpdateAsync(int id, Role role)
        {
            var existingRole  = await roleRepository.FindByIdAsync(id);

            if (existingRole == null)
                return new RoleResponse("Role not found");

            existingRole.Name = role.Name;
     

            try
            {
                roleRepository.Update(existingRole);
                await UnitOfWork.CompleteAsync();
                return new RoleResponse(existingRole);
            }
            catch (Exception ex)
            {
                return new RoleResponse($"Role update error: {ex.Message}");
            }
        }
    }
}