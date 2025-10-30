using Nexta.Domain.Models.Orders;

namespace Nexta.Domain.Specification
{
    public class OrderByUserDataSpecification : BaseSpecification<Order>
    {
        public OrderByUserDataSpecification(string searchTerm, int pageNumber, int pageSize)
        {
            SearchTerm = searchTerm?.ToLower() ?? "";
            AddInclude("Products");
        }
    }
}