using Microsoft.AspNetCore.Mvc;
using PMS.BusinessLayer.Abstract;
using PMS.DTOLayer.ProductDto;
using PMS.Redis.Repository;

namespace Product_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRedisRepository _redisRepository;

        public ProductController(IProductService productService, IRedisRepository redisRepository)
        {
            _productService = productService;
            _redisRepository = redisRepository;
        }

        [HttpGet]
        public IActionResult GetListProduct()
        {
            var result = _productService.GetList();
            return Ok(result);
        }
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);

            if (result == null)
            {
                throw new Exception("Hatalı ID girişi: Ürün bulunamadı.");

            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddProduct(AddProductDto addProductDto)
        {
            _productService.Add(addProductDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _productService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            _productService.Update(updateProductDto);
            return Ok();
        }

        [HttpPut("multi")]
        public IActionResult MultipleUpdateProduct(List<UpdateProductDto> updateProductDto)
        {
            var value = _productService.MultipleUpdateProduct(updateProductDto);
            if (value)
            {
                return Ok();
            }
            return BadRequest("Ürün Bulunamadı");
        }
    }
}