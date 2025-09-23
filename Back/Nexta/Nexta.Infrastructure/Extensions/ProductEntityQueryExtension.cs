using Microsoft.EntityFrameworkCore;
using Nexta.Infrastructure.DataBase.Entities;

namespace Nexta.Infrastructure.Extensions
{
    public static class ServiceCollectionRepositoryExtension
    {
        public static IQueryable<ProductEntity> WithCategoryTerm(this IQueryable<ProductEntity> query, string categoryTerm)
        {
            if (!string.IsNullOrEmpty(categoryTerm))
            {
                query = query.Where(p => p.Category != null && p.Category.ToLower() == categoryTerm);
            }
            return query;
        }

        public static IQueryable<ProductEntity> WithOrderByTerm(this IQueryable<ProductEntity> query, string categoryTerm)
        {
            if (!string.IsNullOrEmpty(categoryTerm))
            {
                query = query.OrderBy(q => categoryTerm);
            }

            return query;
        }

        public static IQueryable<UserEntity> WithSearchTerm(this IQueryable<UserEntity> query, string searchTerm)
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

        public static IQueryable<ProductEntity> WithPriceTerm(this IQueryable<ProductEntity> query, int? minPrice, int? maxPrice)
        {
            if (minPrice != null && maxPrice != null)
            {
                query = query.Where(d => 
                    minPrice <= d.NewPrice &&
                    d.NewPrice <= maxPrice);
            }
            return query;
        }

        public static IQueryable<ProductEntity> WithSearchTerm(this IQueryable<ProductEntity> query, string searchTerm)
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

        public static IQueryable<OrderEntity> WithSearchTerm(this IQueryable<OrderEntity> query, string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(o =>
                    EF.Functions.Like(o.Id.ToString().ToLower(), $"{searchTerm}") ||
                    EF.Functions.Like(o.Id.ToString().ToLower(), $"{searchTerm}%") ||
                    EF.Functions.Like(o.Id.ToString().ToLower(), $"%{searchTerm}") ||

                    EF.Functions.Like(o.User.FirstName.ToLower(), $"{searchTerm}") ||
                    EF.Functions.Like(o.User.FirstName.ToLower(), $"{searchTerm}%") ||
                    EF.Functions.Like(o.User.FirstName.ToLower(), $"%{searchTerm}") ||

                    EF.Functions.Like(o.User.MiddleName.ToLower(), $"%{searchTerm}") ||
                    EF.Functions.Like(o.User.MiddleName.ToLower(), $"%{searchTerm}%") ||
                    EF.Functions.Like(o.User.MiddleName.ToLower(), $"%{searchTerm}") ||

                    EF.Functions.Like(o.User.LastName.ToLower(), $"%{searchTerm}") ||
                    EF.Functions.Like(o.User.LastName.ToLower(), $"%{searchTerm}%") ||
                    EF.Functions.Like(o.User.LastName.ToLower(), $"%{searchTerm}"));
            }
            return query;
        }

        public static IQueryable<NotificationEntity> WithSearchTerm(this IQueryable<NotificationEntity> query, string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(o =>
                    EF.Functions.Like(o.Id.ToString().ToLower(), $"{searchTerm}") ||
                    EF.Functions.Like(o.Id.ToString().ToLower(), $"{searchTerm}%") ||
                    EF.Functions.Like(o.Id.ToString().ToLower(), $"%{searchTerm}") ||

                    EF.Functions.Like(o.User.FirstName.ToLower(), $"{searchTerm}") ||
                    EF.Functions.Like(o.User.FirstName.ToLower(), $"{searchTerm}%") ||
                    EF.Functions.Like(o.User.FirstName.ToLower(), $"%{searchTerm}") ||

                    EF.Functions.Like(o.User.MiddleName.ToLower(), $"%{searchTerm}") ||
                    EF.Functions.Like(o.User.MiddleName.ToLower(), $"%{searchTerm}%") ||
                    EF.Functions.Like(o.User.MiddleName.ToLower(), $"%{searchTerm}") ||

                    EF.Functions.Like(o.User.LastName.ToLower(), $"%{searchTerm}") ||
                    EF.Functions.Like(o.User.LastName.ToLower(), $"%{searchTerm}%") ||
                    EF.Functions.Like(o.User.LastName.ToLower(), $"%{searchTerm}"));
            }
            return query;
        }
    }
}
