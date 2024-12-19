namespace PMS.EntityLayer
{
    public class UserProduct
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User Users { get; set; }
        public Product Products { get; set; }
    }
}