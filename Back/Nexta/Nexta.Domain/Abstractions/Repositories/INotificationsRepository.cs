using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Models.Users;
using Nexta.Domain.Base;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface INotificationsRepository
    {
        Task<PagedData<Notification>> GetAsync(ISpecification<Notification> spec, CancellationToken ct = default);
    }
}