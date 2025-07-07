using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Exceptions;
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

		public async Task<Guid> AddAsync(Image image, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var imageEntity = _mapper.Map<ImageEntity>(image);
				var addedImageId = await context.AddAsync(imageEntity, ct);
				await context.SaveChangesAsync(ct);

				return addedImageId.Entity.Id;
			}
		}

		public async Task<Image> GetByIdAsync(Guid id, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var imageEntity = await context.Images
					.FindAsync(id, ct);

				var image = _mapper.Map<Image>(imageEntity);

				return image;
			}
		}

		public async Task<Image> GetAsync(string name, string bucket, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync())
			{
				var imageEntity = context.Images
					.FirstOrDefaultAsync(i => i.Name == name && i.Bucket == bucket, ct);

				var image = _mapper.Map<Image>(imageEntity);

				return image;
			}
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

		public async Task<Guid> UpdateAsync(Image image, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var imageEntity = await context.Images.FindAsync(image.Id);

				if (imageEntity == null)
					throw new NotFoundException("Картинка не найдена в БД");

				imageEntity.Bucket = image.Bucket;
				imageEntity.Name = image.Name;

				return imageEntity.Id;
			}
		}
	}
}
