using ShopMate.BLL.DTO.AdminDto;
using ShopMate.DAL.Database.Models;

namespace ShopMate.BLL.Mapping
{
    public static class AdminMapping
    {
        public static OrderDto ToOrderDto(this Order order) =>
            new OrderDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus.ToString(),
                OrderDate = order.OrderDate,
                UserFullName = order.User != null ? $"{order.User.FirstName} {order.User.LastName}" : string.Empty
            };

        public static OrderDetailsDto ToOrderDetailsDto(this Order order) =>
            new OrderDetailsDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus.ToString(),
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress != null ? $"{order.ShippingAddress.City}, {order.ShippingAddress.Street}" : string.Empty,
                PaymentMethod = order.PaymentDetails != null ? order.PaymentDetails.PaymentMethod.ToString() : string.Empty,
                CouponCode = order.Coupon != null ? order.Coupon.Code : null,
                UserFullName = order.User != null ? $"{order.User.FirstName} {order.User.LastName}" : string.Empty,
                Items = order.OrderItems?.Select(item => item.ToOrderItemDto()).ToList()
            };

        public static OrderItemDto ToOrderItemDto(this OrderItem orderItem) =>
            new OrderItemDto
            {
                Id = orderItem.Id,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                ProductName = orderItem.Product != null ? orderItem.Product.Name : string.Empty
            };

        public static ContactMessageDto ToContactMessageDto(this ContactMessage message) =>
            new ContactMessageDto
            {
                Id = message.Id,
                Subject = message.Subject,
                Message = message.Message,
                CreatedAt = message.CreatedAt,
                Status = message.Status.ToString(),
                UserFullName = message.User != null ? $"{message.User.FirstName} {message.User.LastName}" : string.Empty
            };

        public static ProductReviewDto ToProductReviewDto(this ProductReview review) =>
            new ProductReviewDto
            {
                Id = review.Id,
                Comment = review.Comment,
                Rating = review.Rating,
                ReviewDate = review.ReviewDate,
                ProductName = review.Product != null ? review.Product.Name : string.Empty,
                UserFullName = review.User != null ? $"{review.User.FirstName} {review.User.LastName}" : string.Empty
            };
    }
}
