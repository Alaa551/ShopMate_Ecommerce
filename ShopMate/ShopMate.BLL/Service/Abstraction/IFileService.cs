using Microsoft.AspNetCore.Http;

namespace ShopMate.BLL.Service.Abstraction
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
        void DeleteFileAsync(string filePath);
    }



}
