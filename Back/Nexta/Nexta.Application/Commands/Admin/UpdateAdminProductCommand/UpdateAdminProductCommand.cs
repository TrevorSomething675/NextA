using Nexta.Application.DTO.Request;
using Nexta.Application.Enums;
using MediatR;

namespace Nexta.Application.Commands.Admin.UpdateAdminProductCommand
{
    public class UpdateAdminProductCommand : IRequest<UpdateAdminProductCommandResponse>
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

		public List<ProductAttributeRequest> Attributes { get; init; } = new List<ProductAttributeRequest>();
		public ProductOperationType Type { get; init; }
		public ProductImageRequest? Image { get; init; }
	}
}