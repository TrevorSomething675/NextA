using Nexta.Application.DTO.Order;
using Nexta.Domain.Filters;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Admin.GetAllOrdersQuery
{
    public class GetAdminOrdersQuery : IRequest<PagedData<OrderDto>>
    {
        public GetOrdersFilter Filter { get; set; }
    }
}