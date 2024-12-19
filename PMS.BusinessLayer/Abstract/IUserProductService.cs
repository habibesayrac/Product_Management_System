using PMS.DTOLayer.UserProductDto;
using PMS.EntityLayer;

namespace PMS.BusinessLayer.Abstract
{
    public interface IUserProductService
    {
        void Add(AddUserProductDto addUserProductDto);
        bool Update(UpdateUserProductDto updateUserProductDto);
        void Delete(int id);
        List<UserProduct> GetList();
        public UserProduct GetById(int id); 
    }
}