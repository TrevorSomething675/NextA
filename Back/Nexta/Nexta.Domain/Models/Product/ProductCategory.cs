using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Product
{
    public class ProductCategory : Entity
    {
        public string Name { get; private set; }

        #pragma warning disable CS8618
        private ProductCategory() { }
        #pragma warning restore CS8618
        public ProductCategory(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}