namespace Nexta.Infrastructure.DataBase.Entities
{
    public class ProductAttributeEntity : BaseEntity
    {
        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}