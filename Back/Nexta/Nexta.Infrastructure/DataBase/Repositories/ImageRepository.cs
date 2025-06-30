using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class ImageRepository : IImageRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		private readonly IMapper _mapper;
		public ImageRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper) 
		{
			_dbContextFactory = dbContextFactory;
			_mapper = mapper;
		}
		public async Task<List<Image>> GetAllAsync(bool isNewsBucketImages = false, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var query = context.Images
					.AsNoTracking();

				if (isNewsBucketImages)
					query = query.Where(i => i.Bucket == "news");

				var imageEntities = await query
					.AsNoTracking()
					.ToListAsync(ct);

				var images = _mapper.Map<List<Image>>(imageEntities);

				return images;
			}
		}
	}
}
