namespace Shop.API.Resources
{
    public class SaveProductResource : IResource
    {
        public string Name { get; set; }
        public int GoodCount { get; set; }
        public int? CategoryId { get; set; }
    }
}