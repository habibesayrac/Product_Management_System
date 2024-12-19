using PMS.DataAccessLayer.Abstract;
using PMS.DataAccessLayer.Concrete;
using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Repository
{
    public class OrderProductRepository : GenericRepository<OrderProduct>, IOrderProductRepository
    {
        private readonly Context _dbContext;
        public OrderProductRepository(Context context) : base(context)
        {
            _dbContext = context;
        }
        public OrderProduct GetById(int id)
        {
            var orderProduct = _dbContext.OrderProducts.FirstOrDefault(x => x.OrderId == id);
            return orderProduct != null ? orderProduct : null;
        }
        
        public override void Delete(int id)
        {
            var orderProduct = _dbContext.OrderProducts.FirstOrDefault(x => x.OrderId == id) ?? throw new Exception("Hatalı ID girişi: Sipariş/Ürün bulunamadı.");

            _dbContext.Remove(id);
            _dbContext.SaveChanges();

        }
        public override bool Update(OrderProduct t)
        {
            var updatedOrderProduct = GetById(t.OrderId);
            if (updatedOrderProduct == null)
            {
                return false;
            }
            return base.Update(updatedOrderProduct);
        }
    }
}