using System.ComponentModel.DataAnnotations;

namespace PMS.EntityLayer
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<UserProduct> UserProducts { get; set; }
        public List<Order> Orders { get; set; }
        
    }
}