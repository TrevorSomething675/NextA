using Nexta.Domain.Enums;

namespace Nexta.Infrastructure.DataBase.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Article { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ProductStatus Status { get; set; }

        public string? Category { get; set; }

        public DateOnly OrderDate { get; set; }
        public DateOnly DeliveryDate { get; set; }

        public int Count { get; set; } = 0;
        public int NewPrice { get; set; }
        public int? OldPrice { get; set; }

        public ICollection<BasketProductEntity> BasketProducts { get; set; } = new List<BasketProductEntity>();
        public ICollection<OrderProductEntity> OrderProducts { get; set; } = new List<OrderProductEntity>();
        public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

        public ICollection<ProductAttributeEntity> Attributes { get; set; } = new List<ProductAttributeEntity>();

        public bool IsVisible { get; set; } = false;

        public ProductImageEntity? Image { get; set; }
    }
}