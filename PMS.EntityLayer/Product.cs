using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PMS.EntityLayer
{
    public class Product : BaseEntity
    {
        [Key]
        public int ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public List<UserProduct> UserProducts { get; set; }
    }
}