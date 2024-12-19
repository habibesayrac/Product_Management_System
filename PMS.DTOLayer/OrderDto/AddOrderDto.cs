namespace PMS.DTOLayer.OrderDto
{
    public class AddOrderDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }

    }
}