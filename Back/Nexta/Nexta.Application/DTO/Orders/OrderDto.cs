using Nexta.Application.DTO.Products;
using Nexta.Domain.Enums;

namespace Nexta.Application.DTO.Orders
{
    public record OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> Products { get; } = new ();
    }

    public record OrderItemDto
    {
        public OrderItemDto(Guid orderId, Guid productId, int count, ProductDto? product = null)
        {
            ProductId = productId;
            Product = product;
            OrderId = orderId;
            Count = count;
        }

        public Guid OrderId { get; }
        public Guid ProductId { get; }
        public ProductDto? Product { get; set; }
        public int Count { get; }
    }
}