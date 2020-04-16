using Shop.API.Domain.Models;
using Shop.API.Domain.Services.Communication;
using Shop.API.Resources;
using Shop.API.Resources.Communication;

namespace Shop.API.Extensions
{
    public static class BaseResponseExtension
    {
        public static ResponseResult GetResponseResult<T>(this T response, IResource resource) where T: BaseResponse 
        {
            return new ResponseResult
            {
                Data = resource,
                Message = response.Message,
                Success = response.Success
            };
        }
    }
}