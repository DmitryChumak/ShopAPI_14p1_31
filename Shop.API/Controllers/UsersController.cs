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
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseResult> GetAllAsync()
        {
            var users = await userService.ListAsync();
            var resources = mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            var result = new ResponseResult
            {
                Data = resources,
                Message = "",
                Success = true
            };
            return result;
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveUserResource, User>(resource);
            var userResponse = await userService.SaveAsync(user);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);
            var result = userResponse.GetResponseResult(userResource);
            return Ok(result);

        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveUserResource, User>(resource);
            var userResponse = await userService.UpdateAsync(id, user);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);
            var result = userResponse.GetResponseResult(userResource);
            return Ok(result);
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            var userResponse = await userService.DeleteAsync(id);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);
            var result = userResponse.GetResponseResult(userResource);
            return Ok(result);
        }
    }
}