using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Abstract
{
    public interface IUserProductRepository:IGenericRepository<UserProduct>
    {
        public UserProduct GetById(int id);

    }
}