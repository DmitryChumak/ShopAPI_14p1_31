using System;
using System.Collections.Generic;
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
    public class UserRolesController : Controller
    {
        private readonly IUserRoleService userRoleService;
        private readonly IMapper mapper;
        public UserRolesController(IUserRoleService userRoleService, IMapper mapper)
        {
            this.userRoleService = userRoleService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ResponseResult> ListAsync(int id)
        {
            var users = await userRoleService.ListUsersByRoleAsync(id);
            var resources = mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
          
            var result = new ResponseResult 
            {
                Success = true,
                Message = "",
                Data = resources
            };
            return result;
        }

        [HttpGet]
        [Route("getrolesbyuser/{id}")]
        public async Task<ResponseResult> GetRolesByUser(int id)
        {       
            var userRoles = await userRoleService.ListRolesByUserAsync(id);

            var result = new ResponseResult{
                Success = true,
                Message = "",
                Data = userRoles.Select(x=>x.Role.Name)
            };
            return result;
        }




        [HttpPost]
        [Route("setrole")]
        public async Task<IActionResult> SetRole([FromBody]SaveUserRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userResponse = await userRoleService.SetUserRoleAsync(resource.UserId, resource.RoleId);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);
            var result = userResponse.GetResponseResult(userResource);
            return Ok(result);

        }

        [HttpDelete]
        [Route("deleterole/{id}")]
        public async Task<IActionResult> DeleteRole(int id, [FromBody] SaveUserRoleResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var userResponse = await userRoleService.DeleteRoleAsync(id, resource.RoleId);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);
            var result = userResponse.GetResponseResult(userResource);
            return Ok(result);
        }
    }
}