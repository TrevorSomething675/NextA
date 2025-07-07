using Nexta.Domain.Models;

namespace Nexta.Domain.Abstractions.Services
{
    public interface IMinioService
    {
        //Task<List<Image>> GetNewsImages(CancellationToken ct = default);
        Task<List<Image>> GetFilesAsync(string bucket, CancellationToken ct = default, params string[] fileNames);

        Task<Image> AddFileAsync(string base64String, string fileName, string bucket, CancellationToken ct = default);
    }
}