using Nexta.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models;

namespace Nexta.Infrastructure.Persistence.Repositories
{
	public class NewsRepository : INewsRepository
	{
		private readonly MainContext _context;

		public NewsRepository(MainContext context)
		{
			_context = context;
		}

		public async Task<News> GetByIdAsync(Guid id, CancellationToken ct = default)
		{
			var news = await _context.News.FirstOrDefaultAsync(n => n.Id == id, ct);
			return news;
		}

		public async Task<List<News>> GetAsync(CancellationToken ct = default)
		{
			var news = await _context.News.ToListAsync(ct);
			return news;
		}

		public async Task<News> AddAsync(News news, CancellationToken ct = default)
		{
			var result = await _context.News.AddAsync(news, ct);
			return result.Entity;
		}
		
		public Guid DeleteAsync(News news, CancellationToken ct = default)
		{
			var result = _context.News.Remove(news);
			return result.Entity.Id;
		}

		public Guid Update(News news, CancellationToken ct = default)
		{
			var result = _context.News.Update(news);
			return result.Entity.Id;
		}
	}
}