namespace ShopMate.BLL.DTO.AdminDto
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }                    
        public string? UserFullName { get; set; }        
        public DateTime OrderDate { get; set; }     
        public decimal TotalAmount { get; set; }         
        public string? OrderStatus { get; set; }          
        public string? ShippingAddress { get; set; }    
        public string? CouponCode { get; set; }     
        public string? PaymentMethod { get; set; }       

        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>(); // تفاصيل المنتجات
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }     
        public int Quantity { get; set; }              
        public decimal UnitPrice { get; set; }         
    }
}
