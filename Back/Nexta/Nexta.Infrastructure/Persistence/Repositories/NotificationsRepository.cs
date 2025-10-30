using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models.Users;
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

        public async Task<PagedData<Notification>> GetAsync(ISpecification<Notification> spec, CancellationToken ct = default)
        {
            var query = _context.Notifications
                .WithSearchTerm(spec.SearchTerm)
                .Where(spec.Creteria)
                .AsNoTracking();

            var notificationEntities = await _context.Notifications
                .Skip((spec.PageNumber - 1) * spec.PageSize)
                .Take(spec.PageSize)
                .ToListAsync(ct);

            var countNotifications = await query.CountAsync(ct);
            var pageCount = (int)Math.Ceiling((double)countNotifications / spec.PageSize);

            var pagedNotifications = new PagedData<Notification>(notificationEntities, notificationEntities.Count, pageCount);

            return pagedNotifications;
        }
    }
}