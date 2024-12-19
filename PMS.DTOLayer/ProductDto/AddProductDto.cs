namespace PMS.DTOLayer.ProductDto
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CategoryId { get; set; }

    }
}