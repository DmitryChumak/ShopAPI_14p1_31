using System.Linq;
using AutoMapper;
using Shop.API.Domain.Models;
using Shop.API.Resources;
using Shop.API.Resources.Communication;

namespace Shop.API.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();
            CreateMap<Product, ProductResource>();
            CreateMap<User, UserResource>()
                .ForMember(x => x.Role, y => y.MapFrom(s => s.UserRoles.Select(z => z.Role.Name)));
            
            CreateMap<Role, RoleResource>();
               
          
            
        }
    }
}