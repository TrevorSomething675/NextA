using Nexta.Domain.Enums;
using Nexta.Domain.Models.Basket;

namespace Nexta.Application.DTO.Response
{
    public class ProductResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string Article { get; init; } = null!;
        public string Description { get; init; } = null!;
        public ProductStatus Status { get; init; }

        public string? Category { get; set; }

        public string OrderDate { get; init; } = string.Empty;
        public string DeliveryDate { get; init; } = string.Empty;

        public int Count { get; init; }
        public int NewPrice { get; init; }
        public int OldPrice { get; init; }
        public bool IsVisible { get; init; }

        public ProductImageResponse? Image { get; init; }
        public List<ProductAttributeResponse> Attributes { get; init; } = new List<ProductAttributeResponse>();
        public List<BasketItem>? BasketProducts { get; set; }
    }
}
