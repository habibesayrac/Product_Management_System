using PMS.BusinessLayer.Abstract;
using PMS.DataAccessLayer.Abstract;
using PMS.DTOLayer.OrderProductDto;
using PMS.EntityLayer;

namespace PMS.BusinessLayer.Concrete
{
    public class OrderProductManager :IOrderProductService
    {
       private readonly IOrderProductRepository _orderProductRepository;

        public OrderProductManager(IOrderProductRepository orderProductRepository)
        {
            _orderProductRepository = orderProductRepository;
        }
        public List<OrderProduct> GetList()
        {
            return _orderProductRepository.GetList();
        }

        public void Add(AddOrderProductDto addOrderProductDto)
        {
            //OrderProduct orderProduct = new OrderProduct()
            //{
            //    OrderId = addOrderProductDto.OrderId,
            //    ProductId = addOrderProductDto.ProductId
            //};
            //_orderProductRepository.Add(orderProduct);
        }

        public void Delete(int id)
        {
            _orderProductRepository.Delete(id);
        }

        public OrderProduct GetById(int id)
        {
            var orderProduct = _orderProductRepository.GetById(id);
            return orderProduct;
        }

        public bool Update(UpdateOrderProductDto updateOrderProductDto)
        {
            var updatedOrderProduct = GetById(updateOrderProductDto.OrderId);
            if (updateOrderProductDto == null)
            {
                return false;
            }

            return _orderProductRepository.Update(updatedOrderProduct);
        }
    }
}