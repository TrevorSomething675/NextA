using Nexta.Domain.Entities;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IImageRepository
    {
        Task<List<ImageEntity>> GetAllAsync(bool isNewsBucketImages = false, CancellationToken ct = default);
    }
}