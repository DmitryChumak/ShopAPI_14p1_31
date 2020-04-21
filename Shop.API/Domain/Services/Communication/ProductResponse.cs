using Shop.API.Domain.Models;

namespace Shop.API.Domain.Services.Communication
{
    public class ProductResponse : BaseResponse
    {
        public Product Product {get; private set;}

        public ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }

        public ProductResponse(Product product) : this(true, string.Empty, product) {}
        
        
        public ProductResponse(string message) : this(false, message, null) {}
    }
}