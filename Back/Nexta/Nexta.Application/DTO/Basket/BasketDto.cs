using Nexta.Application.DTO.Product;

namespace Nexta.Application.DTO.Basket
{
    public record BasketDto
    {
        public Guid UserId { get; init; }
        public IReadOnlyCollection<BasketItemDto> Products { get; init; } = new List<BasketItemDto>().AsReadOnly();
    }

    public record BasketItemDto
    {
        public BasketItemDto(Guid productId, int count, ProductDto? product = null)
        {
            ProductId = productId;
            Product = product;
            Count = count;
        }

        public Guid ProductId { get; init; }
        public ProductDto? Product { get; init; }
        public int Count { get; init; }
    }
}