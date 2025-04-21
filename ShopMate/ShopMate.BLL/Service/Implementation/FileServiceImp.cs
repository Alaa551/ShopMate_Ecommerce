using Microsoft.AspNetCore.Http;
using ShopMate.BLL.Service.Abstraction;

namespace ShopMate.BLL.Service.Implementation
{
    public class FileServiceImp : IFileService
    {
        private const string UploadsFolder = "Uploads";


        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Uploaded file is empty or missing.");


            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), UploadsFolder);
            Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/{UploadsFolder}/{fileName}";
        }

        public void DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("File path is null or empty.");

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error deleting file: {ex.Message}");
                }
            }

        }

    }
}