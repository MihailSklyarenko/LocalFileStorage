using LocalFileStorageData.Models;
using Microsoft.AspNetCore.Http;

namespace LocalFileStorageData.Interfaces
{
    public interface IStorageService
    {
        Task<StorageFileViewModel> AddAsync(IFormFile file, CancellationToken token = default);
        Task<StorageFileViewModel> GetAsync(int id, CancellationToken token = default);
        Task DeleteFileAsync(int id, CancellationToken token = default);
    }
}
