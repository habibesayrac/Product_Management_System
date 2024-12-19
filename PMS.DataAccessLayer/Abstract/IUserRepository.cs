using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Abstract
{
    public interface IUserRepository:IGenericRepository<User>
    {
        public User GetById(int id);

    }
}