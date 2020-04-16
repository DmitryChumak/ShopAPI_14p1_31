using Shop.API.Domain.Models;

namespace Shop.API.Resources
{
    public class ProductResource : IResource
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public int GoodCount { get; set; }

        public CategoryResource Category { get; set; }
    }
}