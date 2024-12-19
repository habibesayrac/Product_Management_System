using Microsoft.AspNetCore.Mvc;
using PMS.BusinessLayer.Abstract;
using PMS.DTOLayer.OrderDto;

namespace Product_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderProductService _orderProductService;

        public OrderController(IOrderService orderService, IOrderProductService orderProductService)
        {
            _orderService = orderService;
            _orderProductService = orderProductService;
        }

        [HttpGet]
        public IActionResult GetListOrder()
        {
            var result = _orderService.GetList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _orderService.GetById(id);
            if (result == null)
            {
                throw new Exception("Hatalı ID girişi: Sipariş bulunamadı.");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddOrder(AddOrderDto addOrderDto)
        {
            try
            {
                _orderService.AddOrder(addOrderDto);
                return Ok("Sipariş ve Sipariş/Ürün başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ekleme başarısız: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            _orderService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateOrder(UpdateOrderDto updateOrderDto)
        {
            _orderService.Update(updateOrderDto);
            return Ok();
        }
    }
}