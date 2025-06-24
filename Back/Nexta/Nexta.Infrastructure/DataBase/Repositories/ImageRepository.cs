using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Entities;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class ImageRepository : IImageRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		public ImageRepository(IDbContextFactory<MainContext> dbContextFactory) 
		{
			_dbContextFactory = dbContextFactory;
		}
		public async Task<List<ImageEntity>> GetAllAsync(bool isNewsBucketImages = false, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var query = context.Images
					.AsNoTracking();

				if (isNewsBucketImages)
					query = query.Where(i => i.Bucket == "news");

				var images = await query
					.AsNoTracking()
					.ToListAsync(ct);

				return images;
			}
		}
	}
}
