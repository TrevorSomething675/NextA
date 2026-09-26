using Nexta.Domain.Filters.Notifications;
using Nexta.Domain.Models.DataModels;
using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface INotificationsRepository
    {
        Task<PagedData<Notification>> GetAsync(GetNotificationsFilter filter, CancellationToken ct = default);
    }
}