using Nexta.Application.DTO.Product;
using MediatR;

namespace Nexta.Application.Queries.Products.GetProductByIdQuery
{
    public class GetProductByIdQuery(Guid id) : IRequest<ProductDto>
    {
        public Guid Id { get; init; } = id;
    }
}
