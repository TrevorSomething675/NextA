using Nexta.Domain.Specification.Abstractions;
using Nexta.Domain.Models.User;
using Nexta.Domain.Base;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface INotificationsRepository
    {
        Task<PagedData<Notification>> GetAsync(ISpecification<Notification> spec, CancellationToken ct = default);
    }
}