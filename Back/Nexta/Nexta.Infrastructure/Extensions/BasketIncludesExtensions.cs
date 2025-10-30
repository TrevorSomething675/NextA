using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Baskets;

namespace Nexta.Infrastructure.Extensions
{
    public static class BasketIncludesExtensions
    {
        public static IQueryable<Basket> Includes(this IQueryable<Basket> basket, List<string> includes)
        {
            foreach (var include in includes)
            {
                basket = basket.Include(include);
            }

            return basket;
        }
    }
}
