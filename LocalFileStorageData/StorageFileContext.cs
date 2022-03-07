using Microsoft.EntityFrameworkCore;
using LocalFileStorageData.Models;

namespace LocalFileStorageData
{
    public class StorageFileContext : DbContext
    {
        public DbSet<StorageFile> Files { get; set; }

        public StorageFileContext(DbContextOptions<StorageFileContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
