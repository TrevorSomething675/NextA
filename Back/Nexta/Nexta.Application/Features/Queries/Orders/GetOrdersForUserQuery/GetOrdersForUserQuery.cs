using Nexta.Application.DTO.Orders;
using Nexta.Domain.Base;
using MediatR;

namespace Nexta.Application.Queries.Orders.GetOrdersForUserQuery
{
    public class GetOrdersForUserQuery : IRequest<PagedData<OrderDto>>
    {
        public string SearchTerm { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 8;
    }
}