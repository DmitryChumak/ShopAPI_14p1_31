using Shop.API.Domain.Models;

namespace Shop.API.Domain.Services.Communication
{
    public class UserResponse : BaseResponse
    {
        public User User {get; private set;}

        public UserResponse(bool success, string message, User user) : base(success, message)
        {
            User = user;
        }

        public UserResponse(User user) : this(true, string.Empty, user) {}
        
        
        public UserResponse(string message) : this(false, message, null) {}
    }
}