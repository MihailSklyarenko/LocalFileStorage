// See https://aka.ms/new-console-template for more information
using LocalFileStorageData;
using LocalFileStorageData.Models;

Console.WriteLine("Hello, World!");


using (StorageFileContext context = new StorageFileContext())
{
    context.Files.Add(new StorageFile() { Name = "test" });
    context.SaveChanges();
}