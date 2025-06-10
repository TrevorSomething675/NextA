using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetLegacyOrdersQuery
{
    public class GetLegacyOrdersQueryRequest : IRequest<GetLegacyOrdersQueryResponse>
    {
		public OrdersFilter Filter { get; set; }
	}
}
