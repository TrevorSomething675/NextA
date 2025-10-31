using Nexta.Application.DTO.Products;
using MediatR;

namespace Nexta.Application.Commands.Admin.CreateAdminProductCommand
{
    public class CreateAdminProductCommand : IRequest<Guid>
    {
        public string Name { get; set; } = null!;
        public string Article { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Status { get; set; }

        public string? Category { get; set; }

        public string? OrderDate { get; set; }
        public string? DeliveryDate { get; set; }

        public int Count { get; set; }
        public int NewPrice { get; set; }
        public int? OldPrice { get; set; }

        public bool IsVisible { get; set; }

        public ProductImageDto? Image { get; set; }
    }
}