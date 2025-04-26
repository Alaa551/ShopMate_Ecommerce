namespace ShopMate.BLL.DTO.AdminDto
{
    public class ProductReviewDto
    {
        public int Id { get; set; }                
        public string? ProductName { get; set; }    
        public string? UserFullName { get; set; }      
        public string? Comment { get; set; }             
        public int Rating { get; set; }                 
        public DateTime ReviewDate { get; set; }       
    }
}
