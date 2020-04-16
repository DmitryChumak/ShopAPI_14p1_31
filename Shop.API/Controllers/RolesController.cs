using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services;
using Shop.API.Extensions;
using Shop.API.Resources;
using Shop.API.Resources.Communication;

namespace Shop.API.Controllers
{
    
    [Route("/api/[controller]")]
    public class RolesController: Controller
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseResult> GetAllAsync()
        {
            var roles = await roleService.ListAsync();
            var resources = mapper.Map<IEnumerable<Role>, IEnumerable<RoleResource>>(roles);
            var result = new ResponseResult
            {
                Data = resources,
                Message = "",
                Success = true
            };
            return result;
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<SaveRoleResource, Role>(resource);
            var roleResponse = await roleService.SaveAsync(role);

            var roleResource = mapper.Map<Role, RoleResource>(roleResponse.Role);
            var result = roleResponse.GetResponseResult(roleResource);
            return Ok(result);

        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<SaveRoleResource, Role>(resource);
            var roleResponse = await roleService.UpdateAsync(id, role);
            var roleResource = mapper.Map<Role, RoleResource>(roleResponse.Role);
            var result = roleResponse.GetResponseResult(roleResource);
            return Ok(result);
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            var roleResponse = await roleService.DeleteAsync(id);
            var roleResource = mapper.Map<Role, RoleResource>(roleResponse.Role);
            var result = roleResponse.GetResponseResult(roleResource);
            return Ok(result);
        }
    }
}