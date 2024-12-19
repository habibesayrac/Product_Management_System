using PMS.DTOLayer.UserDto;
using PMS.EntityLayer;

namespace PMS.BusinessLayer.Abstract
{
    public interface IUserService
    {
        void Add(AddUserDto addUserDto );
        bool Update(UpdateUserDto updateUserDto);
        void Delete(int id);
        List<User> GetList();
        public User GetById(int id);
    }
}