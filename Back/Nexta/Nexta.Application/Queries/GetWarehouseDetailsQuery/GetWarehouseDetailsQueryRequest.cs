using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetWarehouseDetailsQuery
{
    public class GetWarehouseDetailsQueryRequest : IRequest<GetWarehouseDetailsQueryResponse>
    {
        public GetDetailsFilter Filter { get; set; } = null!;
    }
}
