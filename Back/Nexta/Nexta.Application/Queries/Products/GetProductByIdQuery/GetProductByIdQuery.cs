using MediatR;

namespace Nexta.Application.Queries.Products.GetProductByIdQuery
{
    public class GetProductByIdQuery(Guid id) : IRequest<GetProductByIdQueryResponse>
    {
        public Guid Id { get; init; } = id;
    }
}
