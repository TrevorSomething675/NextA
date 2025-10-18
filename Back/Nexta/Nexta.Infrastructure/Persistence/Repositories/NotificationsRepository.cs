using Nexta.Domain.Abstractions.Repositories;
using Nexta.Domain.Filters.Notifications;
using Nexta.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.User;
using Nexta.Domain.Base;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly MainContext _context;

        public NotificationsRepository(MainContext context)
        {
            _context = context;
        }

        public async Task<PagedData<Notification>> GetAsync(GetNotificationsFilter filter, CancellationToken ct = default)
        {
            var searchTerm = filter.SearchTerm.ToLower() ?? "";

            var query = _context.Notifications
                .WithSearchTerm(searchTerm)
                .Where(n => n.UserId == filter.UserId)
                .AsNoTracking();

            var notificationEntities = await _context.Notifications
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync(ct);

            var countNotifications = await query.CountAsync(ct);
            var pageCount = (int)Math.Ceiling((double)countNotifications / filter.PageSize);

            var pagedNotifications = new PagedData<Notification>(notificationEntities, notificationEntities.Count, pageCount);

            return pagedNotifications;
        }
    }
}