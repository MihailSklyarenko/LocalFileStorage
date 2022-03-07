using AutoMapper;
using LocalFileStorageData.Models;

namespace LocalFileStorageData.DependencyInjection
{
    public class FileStorageMappingProfile : Profile
    {
        public FileStorageMappingProfile()
        {
            CreateMap<StorageFile, StorageFileViewModel>();
        }
    }
}
