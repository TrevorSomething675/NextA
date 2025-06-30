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
	}
}
