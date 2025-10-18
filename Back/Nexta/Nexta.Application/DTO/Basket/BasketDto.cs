namespace Nexta.Application.DTO.Basket
{
    public record BasketDto
    {
        public Guid UserId { get; }
        public IReadOnlyCollection<BasketItemDto> Products { get; } = new List<BasketItemDto>().AsReadOnly();
    }

    public record BasketItemDto
    {
        public BasketItemDto(Guid productId, int count)
        {
            ProductId = productId;
            Count = count;
        }

        public Guid ProductId { get; }
        public int Count { get; }
    }
}