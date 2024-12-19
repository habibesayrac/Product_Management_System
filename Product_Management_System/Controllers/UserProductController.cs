using Microsoft.AspNetCore.Mvc;
using PMS.BusinessLayer.Abstract;
using PMS.DTOLayer.UserProductDto;
using PMS.Redis.Repository;

namespace Product_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductController : ControllerBase
    {
        private readonly IUserProductService _userProductService;
        private readonly IRedisRepository _redisRepository;

        public UserProductController(IUserProductService userProductService, IRedisRepository redisRepository)
        {
            _userProductService = userProductService;
            _redisRepository = redisRepository;
        }
        [HttpGet]
        public IActionResult GetListUserProduct()
        {
            var result = _userProductService.GetList();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _userProductService.GetById(id);
            if (result == null)
            {
                throw new Exception("Hatalı ID girişi: Kullanıcı/Ürün bulunamadı.");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddUserProduct(AddUserProductDto addUserProductDto)
        {
            _userProductService.Add(addUserProductDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserProduct(int id)
        {
            _userProductService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateOrderProduct(UpdateUserProductDto updateUserProductDto)
        {
            _userProductService.Update(updateUserProductDto);
            return Ok();
        }
    }
}