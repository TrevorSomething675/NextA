using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.DataBase.Entities;
using Nexta.Domain.Filters.Notifications;
using Nexta.Infrastructure.Extensions;
using Nexta.Domain.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Models;
using AutoMapper;

namespace Nexta.Infrastructure.DataBase.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public NotificationsRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<PagedData<Notification>> GetAsync(GetNotificationsFilter filter, CancellationToken ct = default)
        {
            await using(var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var searchTerm = filter.SearchTerm.ToLower() ?? "";

                var query = context.Notifications
                    .WithSearchTerm(searchTerm)
                    .Where(n => n.UserId == filter.UserId)
                    .AsNoTracking();

                var notificationEntities = (await context.Notifications
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync(ct));

                var countNotifications = await query.CountAsync(ct);
                var pageCount = (int)Math.Ceiling((double)countNotifications / filter.PageSize);

                var pagedNotificationEntities = new PagedData<NotificationEntity>(notificationEntities, notificationEntities.Count, pageCount);

                var pagedNotifications = _mapper.Map<PagedData<NotificationEntity>, PagedData<Notification>>(pagedNotificationEntities);

                return pagedNotifications;
            }
        }
    }
}