using Nexta.Domain.Models.Orders;
using Nexta.Domain.Enums;
using Nexta.Application.Abstractions.Specification;

namespace Nexta.Domain.Specification
{
    public class OrderByStatusSpecification : BaseSpecification<Order>
    {
        public OrderByStatusSpecification(Guid userId, OrderStatus[] status, int pageNumber, int pageSize)
        {
            Creteria = o => status.Contains(o.Status) && o.UserId == userId;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}