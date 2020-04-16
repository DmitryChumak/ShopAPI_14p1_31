using Shop.API.Domain.Models;

namespace Shop.API.Domain.Services.Communication
{
    public class RoleResponse : BaseResponse
    {
        public Role Role {get; private set;}

        public RoleResponse(bool success, string message, Role role) : base(success, message)
        {
            Role = role;
        }

        public RoleResponse(Role role) : this(true, string.Empty, role) {}
        
        
        public RoleResponse(string message) : this(false, message, null) {}
    }
}