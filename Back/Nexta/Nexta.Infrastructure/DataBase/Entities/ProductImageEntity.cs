namespace Nexta.Infrastructure.DataBase.Entities
{
    public class ProductImageEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Base64String { get; set; }

        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}