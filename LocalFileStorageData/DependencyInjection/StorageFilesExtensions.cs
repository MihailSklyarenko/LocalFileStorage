using LocalFileStorageData.Interfaces;
using LocalFileStorageData.Options;
using LocalFileStorageData.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalFileStorageData.DependencyInjection
{
    public static class StorageFilesExtensions
    {
        public static void AddStorageFilesSupport(IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<FileStorageOptions>(configuration.GetSection("FileStorageOptions").Value);
            services.Configure<FileStorageOptions>(x => x.LocalPath = configuration.GetSection("FileStorageOptions:LocalPath").Value);

            services.AddDbContext<StorageFileContext>(options =>
            {
                options.UseSqlite("Data Source=helloapp.db");//Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(typeof(FileStorageMappingProfile));
            services.AddScoped<IStorageService, LocalStorageService>();

        }
    }
}