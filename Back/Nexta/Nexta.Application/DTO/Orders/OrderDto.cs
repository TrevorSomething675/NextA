namespace Nexta.Application.DTO.Orders
{
    public record OrderDto
    {
        public Guid UserId { get; }
        public IReadOnlyCollection<OrderItemDto> Products { get; } = new List<OrderItemDto>().AsReadOnly();
    }

    public record OrderItemDto
    {
        public OrderItemDto(Guid orderId, Guid productId, int count)
        {
            ProductId = productId;
            OrderId = orderId;
            Count = count;
        }

        public Guid OrderId { get; }
        public Guid ProductId { get; }
        public int Count { get; }
    }
}