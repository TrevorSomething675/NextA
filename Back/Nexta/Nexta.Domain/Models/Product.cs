using Nexta.Domain.Models.Images;
using Nexta.Domain.Enums;

namespace Nexta.Domain.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Article { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DetailStatus Status { get; set; }

        public string? OrderDate { get; set; }
        public string? DeliveryDate { get; set; }

        public int Count { get; set; }
        public int NewPrice { get; set; }
        public int? OldPrice { get; set; }

        public bool IsVisible { get; set; }

        public List<BasketProduct>? BasketProducts { get; set; }
        public List<OrderProduct>? OrderProducts { get; set; }
        public List<Order>? Orders { get; set; }

        public Guid? ImageId { get; set; }
        public ProductImage? Image { get; set; }
    }
}