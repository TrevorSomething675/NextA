using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Exceptions;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.DataBase.Repositories
{
	public class NewsRepository : INewsRepository
	{
		private readonly IDbContextFactory<MainContext> _dbContextFactory;
		private readonly IMapper _mapper;

		public NewsRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
		{
			_dbContextFactory = dbContextFactory;
			_mapper = mapper;
		}

		public Task<News> GetByIdAsync(Guid id, CancellationToken ct = default)
		{
			throw new NotImplementedException();
		}

		public async Task<List<News>> GetAllAsync(CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var newsEntities = await context.News
					.Include(n => n.Image)
					.ToListAsync(ct);

				var news = _mapper.Map<List<News>>(newsEntities);
				return news;
			}
		}

		public async Task<News> AddAsync(News news, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var existingNews = await context.News.FirstOrDefaultAsync(n => n.Id == news.Id, ct);

				if (existingNews != null)
					throw new ConflictException($"Новость: [{news.Header}] с id: [{news.Id}] уже существует.");

				var newsEntity = _mapper.Map<NewsEntity>(news);
				var createdNews = await context.News.AddAsync(newsEntity, ct);
				await context.SaveChangesAsync(ct);

				var result = _mapper.Map<News>(createdNews.Entity);
				return result;
			}
		}
		
		public async Task<Guid> DeleteAsync(Guid id, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var existingNews = await context.News.FirstOrDefaultAsync(n => n.Id == id, ct);

				if (existingNews == null)
					throw new NotFoundException($"Новость с Id: [{id}] не найдена.");

				context.News.Remove(existingNews);
				await context.SaveChangesAsync(ct);

				return id;
			}
		}

		public async Task<Guid> UpdateAsync(News news, CancellationToken ct = default)
		{
			await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
			{
				var existingNews = await context.News.FirstOrDefaultAsync(n => n.Id == news.Id, ct);

				if (existingNews == null)
					throw new NotFoundException($"Новость с Id: [{news.Id}] не найдена.");

				if (news.Header != null) existingNews.Header = news.Header;
				if (news.Description != null) existingNews.Description = news.Description;

				context.News.Update(existingNews);
				await context.SaveChangesAsync(ct);

				return existingNews.Id;
			}
		}
	}
}