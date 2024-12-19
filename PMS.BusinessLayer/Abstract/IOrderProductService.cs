using PMS.DTOLayer.OrderProductDto;
using PMS.EntityLayer;

namespace PMS.BusinessLayer.Abstract
{
    public interface IOrderProductService
    {
        void Add(AddOrderProductDto addOrderProductDto);
        bool Update(UpdateOrderProductDto updateOrderProductDto);
        void Delete(int id);
        List<OrderProduct> GetList();
        public OrderProduct GetById(int id);
    }
}