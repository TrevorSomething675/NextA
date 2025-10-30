using Nexta.Application.DTO.Products;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductByIdQuery
{
    public class GetProductByIdQuery(Guid id) : IRequest<ProductDto>
    {
        public Guid Id { get; init; } = id;
    }
}
