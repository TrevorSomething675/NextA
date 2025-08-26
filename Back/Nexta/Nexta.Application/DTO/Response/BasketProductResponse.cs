using Nexta.Domain.Enums;

namespace Nexta.Application.DTO.Response
{
    public class BasketProductResponse
    {
        public Guid UserId { get; init; }
        public Guid ProductId { get; init; }
        public int Count { get; init; }
        public DateOnly DeliveryDate { get; set; }
        public UserDetailStatus Status { get; set; }
    }
}
