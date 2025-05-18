using ShopMate.BLL.DTO.AdminDto;
using ShopMate.BLL.Mapping;
using ShopMate.BLL.Service.Abstraction;
using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Repository.Abstraction;

namespace ShopMate.BLL.Service.Implementation
{
    public class ProductReviewServiceImp : IProductReviewService
    {
        private readonly IProductReviewRepo _reviewRepo;

        public ProductReviewServiceImp(IProductReviewRepo reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        public async Task<List<ProductReviewDto>> GetAllReviewsAsync()
        {
            var reviews = await _reviewRepo.GetAllReviewsAsync();
            return reviews.Select(r => r.ToProductReviewDto()).ToList();
        }

        public async Task<ProductReviewDto> GetReviewByIdAsync(int id)
        {
            var review = await _reviewRepo.GetReviewByIdAsync(id);
            return review?.ToProductReviewDto();
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            await _reviewRepo.DeleteReviewAsync(reviewId);
            return true;
        }

       
        public async Task<List<ProductReviewDto>> GetReviewsByProductIdAsync(int productId)
        {
            var reviews = await _reviewRepo.GetReviewsByProductIdAsync(productId);
            return reviews.Select(r => r.ToProductReviewDto()).ToList();
        }
    }
}
