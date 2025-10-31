using MediatR;
using Nexta.Application.DTO.Products;

namespace Nexta.Application.Queries.Admin.GetProductQuery
{
    public class GetAdminProductQuery(Guid productId, bool withImage) : IRequest<AdminProductDto>
    {
        public Guid ProductId { get; init; } = productId;
        public bool WithImage { get; init; } = withImage;
    }
}