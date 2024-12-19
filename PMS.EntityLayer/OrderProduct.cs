using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.EntityLayer
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("OrderId")]
        public Order Orders { get; set; }
        [ForeignKey("ProductId")]
        public Product Products { get; set; }
    }
}