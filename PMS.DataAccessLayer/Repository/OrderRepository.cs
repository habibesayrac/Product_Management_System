using PMS.DataAccessLayer.Abstract;
using PMS.DataAccessLayer.Concrete;
using PMS.DTOLayer.OrderDto;
using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly Context _dbContext;

        public OrderRepository(Context context) : base(context)
        {
            _dbContext = context;
        }

        public Order GetById(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.OrderId == id);
            return order != null ? order : null;
        }
        public void AddOrder(AddOrderDto addOrderDto)
        {
            var isExist= _dbContext.Orders.Any(x=>x.OrderId==addOrderDto.OrderId);
            if (!isExist)
            {
                var newOrder = new Order()
                {
                    OrderId = addOrderDto.OrderId,
                    CreatedDate = DateTime.Now,
                    Description = addOrderDto.Description,
                    Name = addOrderDto.Name,
                    OrderDate = DateTime.Now,
                    UserId = addOrderDto.UserId
                };

                _dbContext.Orders.Add(newOrder);
            }
            var isUserExist = _dbContext.Users.Any(x=>x.UserId==addOrderDto.UserId);
            if (!isUserExist)
            {
                throw new Exception("Böyle bir kullanıcı bulunmamaktadır.");

            }           

            var isExistProductOrder = _dbContext.OrderProducts.Any(x=>x.ProductId==addOrderDto.ProductId&& x.OrderId==addOrderDto.OrderId);
            if (isExistProductOrder)
            {
                throw new Exception("Böyle bir kullanıcı/sipariş bulunmaktadır.");

            }
            var newOrderProduct = new OrderProduct()
            {
                OrderId = addOrderDto.OrderId,
                ProductId = addOrderDto.ProductId                
              
            };
            _dbContext.OrderProducts.Add(newOrderProduct);

            _dbContext.SaveChanges();

        }
        public override void Delete(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.OrderId == id) ?? throw new Exception("Hatalı ID girişi: Sipariş bulunamadı.");
            _dbContext.Remove(order);
            _dbContext.SaveChanges();
        }
        public override bool Update(Order t)
        {
            var updatedOrder = GetById(t.OrderId);
            if (updatedOrder == null)
            {
                return false;
            }

            updatedOrder.OrderDate = DateTime.Now;
            return base.Update(updatedOrder);
        }
    }
}