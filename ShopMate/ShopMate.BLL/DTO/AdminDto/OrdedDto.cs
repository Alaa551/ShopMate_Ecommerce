namespace ShopMate.BLL.DTO.AdminDto
{
    public class OrderDto
    {
        public int Id { get; set; }                
        public string? UserFullName { get; set; }     
        public DateTime OrderDate { get; set; }      
        public decimal TotalAmount { get; set; }    
        public string? OrderStatus { get; set; }      
    }
}
