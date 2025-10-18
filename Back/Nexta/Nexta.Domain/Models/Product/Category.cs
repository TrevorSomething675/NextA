using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Product
{
    public class Category : Entity
    {
        public string Name { get; private set; }

        #pragma warning disable CS8618
        private Category() { }
        #pragma warning restore CS8618
        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}