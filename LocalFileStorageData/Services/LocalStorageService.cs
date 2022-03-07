using AutoMapper;
using LocalFileStorageData.Interfaces;
using LocalFileStorageData.Models;
using LocalFileStorageData.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LocalFileStorageData.Services
{
    public class LocalStorageService : IStorageService
    {
        private readonly FileStorageOptions _fileStorageOptions;
        private readonly StorageFileContext _context;
        private readonly IMapper _mapper;

        public LocalStorageService(IOptions<FileStorageOptions> fileStorageOptions, StorageFileContext context, IMapper mapper)
        {
            _fileStorageOptions = fileStorageOptions.Value;
            _context = context;
            _mapper = mapper;
        }

        public async Task<StorageFileViewModel> AddAsync(IFormFile file, CancellationToken token = default)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = Guid.NewGuid() + extension;
            var path = GetPath(fileName);

            await SaveFileAsync(path, file, token);
            var storage = new StorageFile
            {
                Name = fileName,
                MimeType = file.ContentType,
                Extension = extension,
                Path = fileName,
                Url = ""
            };

            await _context.Files.AddAsync(storage, token);
            await _context.SaveChangesAsync(token);

            return _mapper.Map<StorageFileViewModel>(storage);
        }

        public async Task<StorageFileViewModel> GetAsync(int id, CancellationToken token = default)
        {
            var file = await GetFileFromDbAsync(id, token);
            return _mapper.Map<StorageFileViewModel>(file);
        }

        public async Task DeleteFileAsync(int id, CancellationToken token = default)
        {
            var file = await GetFileFromDbAsync(id, token);
            var path = GetPath(file.Path);

            File.Delete(path);
            _context.Files.Remove(file);
            await _context.SaveChangesAsync(token);
        }

        private async Task<StorageFile> GetFileFromDbAsync(int id, CancellationToken token = default)
        {
            var dbFile = await _context.Files.SingleOrDefaultAsync(x => x.Id == id);
            if (dbFile == null)
            {
                throw new Exception("File not found");
            }

            var path = GetPath(dbFile.Path);
            if (!File.Exists(path))
            {
                throw new Exception("File not found");
            }
            return dbFile;
        }

        private string GetPath(string storedPath) => Path.Combine(_fileStorageOptions.LocalPath, storedPath);

        private static async Task SaveFileAsync(string path, IFormFile file, CancellationToken token = default)
        {
            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, token);
            var stream = memoryStream.ToArray();
            await File.WriteAllBytesAsync(path, stream, token);
        }
    }
}
