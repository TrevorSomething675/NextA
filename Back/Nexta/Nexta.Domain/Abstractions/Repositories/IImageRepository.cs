using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Repositories
{
    public interface IImageRepository
    {
        Task<Image> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Image> GetAsync(string name, string bucket, CancellationToken ct = default);
        Task<List<Image>> GetAllAsync(bool isNewsBucketImages = false, CancellationToken ct = default);

        Task<Image> AddAsync(Image image, CancellationToken ct = default);
        Task<Guid> UpdateAsync(Image image, CancellationToken ct = default);
    }
}