using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Details.GetWarehouseDetailsQuery
{
    public class GetWarehouseDetailsQueryRequest : IRequest<GetWarehouseDetailsQueryResponse>
    {
        public GetDetailsFilter Filter { get; init; } = null!;
    }
}
