using System.Threading.Tasks;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services.Communication;

namespace Shop.API.Domain.Services
{
    public interface IAuthenticationService
    {
        Task<UserResponse> AuthenticateAsync(string login, string password);
         
    }
}