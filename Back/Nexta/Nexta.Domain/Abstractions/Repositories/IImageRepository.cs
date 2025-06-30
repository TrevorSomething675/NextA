using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IImageRepository
    {
        Task<List<Image>> GetAllAsync(bool isNewsBucketImages = false, CancellationToken ct = default);
    }
}