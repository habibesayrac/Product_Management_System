using System.ComponentModel.DataAnnotations;

namespace PMS.EntityLayer
{
    public class Order:BaseEntity
    {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}