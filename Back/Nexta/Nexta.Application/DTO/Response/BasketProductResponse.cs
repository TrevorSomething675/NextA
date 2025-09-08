using Nexta.Domain.Enums;

namespace Nexta.Application.DTO.Response
{
    public class BasketProductResponse
    {
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public string Description { get; init; }
        public int Count { get; init; }
        public DateOnly DeliveryDate { get; set; }
        public string Article { get; set; }
        public string Name { get; set; }

        public BasketProductStatus Status { get; set; }
        public int NewPrice { get; set; }
        public int OldPrice { get; set; }
    }
}