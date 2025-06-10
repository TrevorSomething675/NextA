using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetWarehouseDetailsQuery
{
    public class GetWarehouseDetailsQueryRequest : IRequest<GetWarehouseDetailsQueryResponse>
    {
        public DetailsFilter Filter { get; set; } = null!;
    }
}
