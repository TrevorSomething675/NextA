using Nexta.Domain.Enums;

namespace Nexta.Domain.Models
{
    public class BasketProduct
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public int? Count { get; set; }
        public DateOnly? DeliveryDate { get; set; }
        public BasketProductStatus? Status { get; set; }
    }
}
