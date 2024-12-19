using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Abstract
{
    public interface IOrderProductRepository:IGenericRepository<OrderProduct>
    {
        public OrderProduct GetById(int id);

    }
}