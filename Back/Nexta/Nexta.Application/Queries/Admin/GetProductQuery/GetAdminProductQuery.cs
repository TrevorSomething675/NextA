using MediatR;

namespace Nexta.Application.Queries.Admin.GetProductQuery
{
    public class GetAdminProductQuery(Guid productId, bool withImage) : IRequest<GetAdminProductQueryResponse>
    {
        public Guid ProductId { get; init; } = productId;
        public bool WithImage { get; init; } = withImage;
    }
}