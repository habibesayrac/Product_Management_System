using Microsoft.AspNetCore.Mvc;
using PMS.BusinessLayer.Abstract;
using PMS.DTOLayer.UserDto;
using PMS.Redis.Repository;

namespace Product_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRedisRepository _redisRepository;

        public UserController(IUserService userService, IRedisRepository redisRepository)
        {
            _userService = userService;
            _redisRepository = redisRepository;
        }
        [HttpGet]
        public IActionResult GetListUser()
        {
                 var result = _userService.GetList();
            return Ok(result);
        }
        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);

            if (result == null)
            {
                throw new Exception("Hatalı ID girişi: Kullanıcı bulunamadı.");

            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddUser(AddUserDto addUserDto)
        {
            _userService.Add(addUserDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUser(UpdateUserDto updateUserDto)
        {
            _userService.Update(updateUserDto);
            return Ok();
        }
    }
}