namespace Nexta.Application.DTO.Order
{
    public record OrderDto
    {
        public Guid UserId { get; }
        public IReadOnlyCollection<OrderItemDto> Products { get; } = new List<OrderItemDto>().AsReadOnly();
    }

    public record OrderItemDto
    {
        public OrderItemDto(Guid productId, int count)
        {
            ProductId = productId;
            Count = count;
        }

        public Guid ProductId { get; }
        public int Count { get; }
    }
}
