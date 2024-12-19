using PMS.DTOLayer.OrderDto;
using PMS.EntityLayer;

namespace PMS.BusinessLayer.Abstract
{
    public interface IOrderService
    {
        void AddOrder(AddOrderDto addOrderDto);       
        bool Update(UpdateOrderDto updateOrderDto);
        void Delete(int id);
        List<Order> GetList();
        public Order GetById(int id);
    }
}