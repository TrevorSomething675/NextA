using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Orders.GetLegacyOrdersQuery
{
    public class GetLegacyOrdersQueryRequest : IRequest<GetLegacyOrdersQueryResponse>
    {
		public GetOrdersFilter Filter { get; init; } = null!;
	}
}
