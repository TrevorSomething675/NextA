using Nexta.Domain.Enums;

namespace Nexta.Infrastructure.DataBase.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Article { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ProductStatus Status { get; set; }

        public DateOnly OrderDate { get; set; }
        public DateOnly DeliveryDate { get; set; }

        public int Count { get; set; } = 0;
        public int NewPrice { get; set; }
        public int? OldPrice { get; set; }

        public List<BasketProductEntity>? BasketProducts { get; set; }
        public List<OrderProductEntity>? OrderProducts { get; set; }
        public List<OrderEntity>? Orders { get; set; }

        public bool IsVisible { get; set; } = false;

        public Guid? ImageId { get; set; }
        public ProductImageEntity? Image { get; set; }
    }
}
