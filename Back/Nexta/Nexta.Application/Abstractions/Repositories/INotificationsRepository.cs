using Nexta.Domain.Models.Users;
using Nexta.Domain.Base;
using Nexta.Application.Abstractions.Specification;

namespace Nexta.Application.Abstractions.Repositories
{
    public interface INotificationsRepository
    {
        Task<PagedData<Notification>> GetAsync(ISpecification<Notification> spec, CancellationToken ct = default);
    }
}