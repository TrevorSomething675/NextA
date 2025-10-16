using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Product
{
    public class ProductAttribute : Entity
    {
        public Guid ProductId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        #pragma warning disable CS8618
        private ProductAttribute() { }
        #pragma warning restore CS8618

        public ProductAttribute(Guid productId, string key, string value)
        {
            Key = key;
            Value = value;
            ProductId = productId;
        }
    }
}