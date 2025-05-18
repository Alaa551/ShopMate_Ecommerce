using ShopMate.BLL.DTO.AdminDto;

namespace ShopMate.BLL.Service.Abstraction
{
    public interface IProductReviewService
    {
        Task<List<ProductReviewDto>> GetAllReviewsAsync();
        Task<ProductReviewDto> GetReviewByIdAsync(int id);
        Task<List<ProductReviewDto>> GetReviewsByProductIdAsync(int productId);

        Task<bool> DeleteReviewAsync(int reviewId);
    }
}
