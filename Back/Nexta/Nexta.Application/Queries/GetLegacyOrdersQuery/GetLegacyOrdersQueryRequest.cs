using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetLegacyOrdersQuery
{
    public class GetLegacyOrdersQueryRequest : IRequest<Result<GetLegacyOrdersQueryResponse>>
    {
		public OrdersFilter Filter { get; set; }
	}
}
