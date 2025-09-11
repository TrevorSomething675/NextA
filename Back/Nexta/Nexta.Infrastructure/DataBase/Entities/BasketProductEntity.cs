using Nexta.Domain.Enums;

namespace Nexta.Infrastructure.DataBase.Entities
{
    public class BasketProductEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; } = null!;

        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; } = null!;

        public int Count { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public BasketProductStatus Status { get; set; }
    }
}
