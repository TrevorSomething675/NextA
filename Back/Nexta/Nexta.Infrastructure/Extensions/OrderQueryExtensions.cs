using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Orders;

namespace Nexta.Infrastructure.Extensions
{
    public static class OrderQueryExtensions
    {
        public static IQueryable<Order> Includes(this IQueryable<Order> basket, List<string> includes)
        {
            foreach (var include in includes)
            {
                basket = basket.Include(include);
            }

            return basket;
        }
    }
}
