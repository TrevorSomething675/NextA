using Nexta.Application.DTO.Product;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateAdminProductCommand
{
    public class UpdateAdminProductCommand : IRequest<AdminProductDto>
    {
		public Guid Id { get; init; }
		public string Name { get; init; } = null!;
		public string Article { get; init; } = null!;
		public string Description { get; init; } = null!;
		public int Status { get; init; }

        public string? Category { get; set; }

        public int Count { get; init; }
		public int NewPrice { get; init; }
		public int OldPrice { get; init; }

		public bool IsVisible { get; init; }

		public List<ProductAttributeDto> Attributes { get; init; } = new List<ProductAttributeDto>();
		public ProductImageDto? Image { get; init; }
	}
}