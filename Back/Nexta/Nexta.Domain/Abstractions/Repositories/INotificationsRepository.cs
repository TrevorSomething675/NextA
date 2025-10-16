using Nexta.Domain.Filters.Notifications;
using Nexta.Domain.Base;
using Nexta.Domain.Models.User;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface INotificationsRepository
    {
        Task<PagedData<Notification>> GetAsync(GetNotificationsFilter filter, CancellationToken ct = default);
    }
}