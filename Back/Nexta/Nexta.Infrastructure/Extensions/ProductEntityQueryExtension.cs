using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Order;
using Nexta.Domain.Models.Product;
using Nexta.Domain.Models.User;

namespace Nexta.Infrastructure.Extensions
{
    public static class ServiceCollectionRepositoryExtension
    {
        public static IQueryable<Product> WithCategoryTerm(this IQueryable<Product> query, string categoryTerm)
        {
            if (!string.IsNullOrEmpty(categoryTerm))
            {
                query = query.Where(p => p.Category != null && p.Category.ToLower() == categoryTerm);
            }
            return query;
        }

        public static IQueryable<Product> WithOrderByTerm(this IQueryable<Product> query, string categoryTerm)
        {
            if (!string.IsNullOrEmpty(categoryTerm))
            {
                query = query.OrderBy(q => categoryTerm);
            }

            return query;
        }

        public static IQueryable<User> WithSearchTerm(this IQueryable<User> query, string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u =>
                    EF.Functions.Like(u.Email.ToLower(), $"{searchTerm}") ||
                    EF.Functions.Like(u.Email.ToLower(), $"{searchTerm}%") ||
                    EF.Functions.Like(u.Email.ToLower(), $"%{searchTerm}") ||

                    EF.Functions.Like(u.FirstName.ToLower(), $"{searchTerm}%") ||
                    EF.Functions.Like(u.FirstName.ToLower(), $"%{searchTerm}") ||
                    EF.Functions.Like(u.FirstName.ToLower(), $"%{searchTerm}") ||

                    EF.Functions.Like(u.MiddleName.ToLower(), $"{searchTerm}%") ||
                    EF.Functions.Like(u.MiddleName.ToLower(), $"%{searchTerm}") ||
                    EF.Functions.Like(u.MiddleName.ToLower(), $"%{searchTerm}") ||

                    EF.Functions.Like(u.LastName.ToLower(), $"{searchTerm}%") ||
                    EF.Functions.Like(u.LastName.ToLower(), $"%{searchTerm}") ||
                    EF.Functions.Like(u.LastName.ToLower(), $"%{searchTerm}"));
            }
            return query;
        }

        public static IQueryable<Product> WithPriceTerm(this IQueryable<Product> query, int? minPrice, int? maxPrice)
        {
            if (minPrice != null && maxPrice != null)
            {
                query = query.Where(d => 
                    minPrice <= d.NewPrice &&
                    d.NewPrice <= maxPrice);
            }
            return query;
        }

        public static IQueryable<Product> WithSearchTerm(this IQueryable<Product> query, string searchTerm)
		{
			if (!string.IsNullOrEmpty(searchTerm))
			{
				query = query.Where(d =>
					EF.Functions.Like(d.Article.ToLower(), $"{searchTerm}") ||
					EF.Functions.Like(d.Article.ToLower(), $"{searchTerm}%") ||
					EF.Functions.Like(d.Article.ToLower(), $"%{searchTerm}") ||
					EF.Functions.Like(d.Name.ToLower(), $"{searchTerm}") ||
					EF.Functions.Like(d.Name.ToLower(), $"{searchTerm}%") ||
					EF.Functions.Like(d.Name.ToLower(), $"%{searchTerm}"));
			}
			return query;
		}

        public static IQueryable<Notification> WithSearchTerm(this IQueryable<Notification> query, string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(o =>
                    EF.Functions.Like(o.Id.ToString().ToLower(), $"{searchTerm}") ||
                    EF.Functions.Like(o.Id.ToString().ToLower(), $"{searchTerm}%") ||
                    EF.Functions.Like(o.Id.ToString().ToLower(), $"%{searchTerm}") ||
            }
            return query;
        }
    }
}
