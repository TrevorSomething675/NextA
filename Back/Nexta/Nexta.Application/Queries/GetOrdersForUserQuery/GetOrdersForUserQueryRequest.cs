using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Filters;
using MediatR;

namespace Nexta.Application.Queries.GetOrdersQuery
{
    public class GetOrdersForUserQueryRequest : IRequest<Result<GetOrdersForUserQueryResponse>>
    {
        public OrdersFilter Filter { get; set; }
    }
}