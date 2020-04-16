using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Shop.API.Domain.Models;
using Shop.API.Domain.Services;
using Shop.API.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Shop.API.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Shop.API.Domain.Services.Communication;
using Shop.API.Extensions;

namespace Shop.API.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings appSettings;
        private readonly IUserRepository userRepository;
        public AuthenticationService(IOptions<AppSettings> appSettings,
                           IUserRepository userRepository)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
        }
        public async Task<UserResponse> AuthenticateAsync(string login, string password)
        {
            User user = (await userRepository.ListAsync())
                            .SingleOrDefault(usr => usr.Login == login && usr.Password == password);

            if (user == null)
                return new UserResponse("Invalid login or password");

            try
            {         
                user.GenerateTokenString(appSettings.Secret, appSettings.TokenExpires);
                user.Password = null;
                return new UserResponse(user);

            }
            catch (Exception ex)
            {
                 return new UserResponse($"An error occured when authenticating user: {ex.Message}");
            }


        }
    }
}