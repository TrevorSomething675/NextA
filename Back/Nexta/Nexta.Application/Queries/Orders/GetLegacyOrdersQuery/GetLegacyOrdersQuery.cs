using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.Orders.GetLegacyOrdersQuery
{
    public class GetLegacyOrdersQuery : IRequest<GetLegacyOrdersQueryResponse>
    {
		public GetOrdersFilter Filter { get; init; } = null!;
	}
}