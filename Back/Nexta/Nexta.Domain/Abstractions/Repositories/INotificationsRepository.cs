using Nexta.Domain.Filters.Notifications;
using Nexta.Domain.Models.User;
using Nexta.Domain.Base;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface INotificationsRepository
    {
        Task<PagedData<Notification>> GetAsync(GetNotificationsFilter filter, CancellationToken ct = default);
    }
}