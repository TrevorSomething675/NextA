using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Product
{
    public class ProductImage : Entity
    {
        public ProductImage(Guid productId, string name, string base64String)
        {
            Base64String = base64String;
            ProductId = productId;
            Name = name;
        }

        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public string Base64String { get; private set; }
    }
}