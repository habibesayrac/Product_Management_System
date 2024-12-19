using PMS.DTOLayer.OrderDto;
using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Abstract
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        public Order GetById(int id);
        public void AddOrder(AddOrderDto addOrderDto);

    }
}