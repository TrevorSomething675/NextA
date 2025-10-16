using Nexta.Domain.Enums;
using Nexta.Domain.Base;

namespace Nexta.Domain.Models.Product
{
    public class Product : Entity, IAggregateRoot
    {
        private readonly List<ProductAttribute> _attributes = new List<ProductAttribute>();
        private readonly List<ProductImage> _images = new List<ProductImage>();

        #pragma warning disable CS8618
        private Product() { }
        #pragma warning restore CS8618

        public Product(string name, string article, string description, int? count,
                int newPrice, int? oldPrice, bool? isVisible
            )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name is required", nameof(name));
            if (string.IsNullOrWhiteSpace(article))
                throw new ArgumentNullException("Article is required", nameof(article));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException("Description is required", nameof(description));
            if (newPrice != default)
                throw new ArgumentNullException("NewPrice is required", nameof(newPrice));

            Id = Guid.NewGuid();
            Name = name;
            Article = article;
            Description = description;
            Count = count ?? 0;
            NewPrice = newPrice;
            OldPrice = oldPrice;
            IsVisible = isVisible ?? false;
        }

        public string Name { get; }
        public string Article { get; }
        public string Description { get; }
        public ProductStatus Status { get; }

        public string? Category { get; }

        public int Count { get; } = 0;
        public int NewPrice { get; }
        public int? OldPrice { get; }
        public bool IsVisible { get; } = false;

        public void AddAttribute(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException("[AddAttribute] Key is required", nameof(key));
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("[AddAttribute] Value is required", nameof(value));
        }

        public void AddImage(string name, string base64String)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("[AddImage] Name is required", nameof(name));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("[Base64String] Image payload is required");

            var imageToAdd = new ProductImage(Id, name, base64String);
            _images.Add(imageToAdd);
        }

        public IReadOnlyCollection<ProductAttribute> Attributes => _attributes.AsReadOnly();
        public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();
    }
}