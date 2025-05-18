using Microsoft.EntityFrameworkCore;
using ShopMate.DAL.Database;
using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Repository.Abstraction;

namespace ShopMate.DAL.Repository.Implementation
{
    public class ProductReviewRepoImp : IProductReviewRepo
    {
        private readonly AppDbContext _context;

        public ProductReviewRepoImp(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductReview>> GetAllReviewsAsync()
        {
            return await _context.ProductReviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductReview>> GetReviewsByProductIdAsync(int productId)
        {
            return await _context.ProductReviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)
                .OrderByDescending(r => r.ReviewDate)
                .ToListAsync();
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            var review = await _context.ProductReviews.FindAsync(reviewId);
            if (review != null)
            {
                _context.ProductReviews.Remove(review);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ProductReview?> GetReviewByIdAsync(int id)
        {
            return await _context.ProductReviews
                                 .Include(r => r.Product)
                                 .Include(r => r.User)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

    }
}
