using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;

namespace Nexta.Infrastructure.Extensions
{
    public static class DetailEntityQueryExtension
	{
		public static IQueryable<DetailEntity> WithSearchTerm(this IQueryable<DetailEntity> query, string searchTerm)
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
    }
}
