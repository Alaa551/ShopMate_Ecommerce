namespace ShopMate.BLL.DTO.AdminDto
{
    public class ContactMessageDto
    {
        public int Id { get; set; }                    
        public string? UserFullName { get; set; }      
        public string? Subject { get; set; }             
        public string? Message { get; set; }           
        public DateTime CreatedAt { get; set; }         
        public string? Status { get; set; }              
    }

    public class ContactMessageReplyDto
    {
        public string? MessageId { get; set; }               
        public string? Reply { get; set; }                
    }
}
