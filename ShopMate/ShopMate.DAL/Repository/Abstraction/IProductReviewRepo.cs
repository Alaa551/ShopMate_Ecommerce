using ShopMate.DAL.Database.Models;

namespace ShopMate.DAL.Repository.Abstraction
{
    public interface IProductReviewRepo
    {
        Task<IEnumerable<ProductReview>> GetAllReviewsAsync();

        Task<IEnumerable<ProductReview>> GetReviewsByProductIdAsync(int productId);
        Task<ProductReview?> GetReviewByIdAsync(int id);
        Task DeleteReviewAsync(int reviewId);
    }
}
