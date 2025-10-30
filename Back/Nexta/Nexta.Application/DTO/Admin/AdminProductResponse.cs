using Nexta.Application.DTO.Products;
using Nexta.Domain.Enums;

namespace Nexta.Application.DTO.Admin
{
    public class AdminProductResponse
    {
		public Guid Id { get; init; }
		public string Name { get; init; } = null!;
		public string Article { get; init; } = null!;
		public string Description { get; init; } = null!;
		public ProductStatus Status { get; init; }

		public string Category { get; init; }

		public string OrderDate { get; init; } = string.Empty;
		public string DeliveryDate { get; init; } = string.Empty;

		public int Count { get; init; }
		public int NewPrice { get; init; }
		public int OldPrice { get; init; }
		public bool IsVisible { get; init; }


        public List<ProductAttributeDto> Attributes { get; init; } = new List<ProductAttributeDto>();
        public ProductImageDto Image { get; init; }
	}
}