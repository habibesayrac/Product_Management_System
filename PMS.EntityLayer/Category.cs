using System.ComponentModel.DataAnnotations;

namespace PMS.EntityLayer
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }

        public List<Product> Products { get; set; }
    }
}