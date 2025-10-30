using Nexta.Application.DTO.Products;

namespace Nexta.Web.Areas.Models
{
    public class UpdateAdminProductRequest
    {
		public Guid Id { get; init; }
		public string Name { get; init; } = null!;
		public string Article { get; init; } = null!;
		public string Description { get; init; } = null!;
		public int Status { get; init; }

		public int Count { get; init; }
		public int NewPrice { get; init; }
		public int OldPrice { get; init; }

		public bool IsVisible { get; init; }

        public string? Category { get; set; }

        public List<ProductAttributeDto>? Attributes { get; init; }

        public Guid? ImageId { get; set; }
        public string? ImageName { get; set; }
        public string? ImageBase64String { get; set; }
	}
}