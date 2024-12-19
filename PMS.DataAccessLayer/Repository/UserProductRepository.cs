using PMS.DataAccessLayer.Abstract;
using PMS.DataAccessLayer.Concrete;
using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Repository
{
    public class UserProductRepository : GenericRepository<UserProduct>, IUserProductRepository
    {
        private readonly Context _dbContext;

        public UserProductRepository(Context context) : base(context)
        {
            _dbContext = context;
        }

        public UserProduct GetById(int id)
        {
            var userProduct = _dbContext.UserProducts.FirstOrDefault(x => x.UserId == id);
            return userProduct != null ? userProduct : null;
        }
        public override void Add(UserProduct userProduct)
        {
            if ((userProduct.ProductId == 0 || userProduct.ProductId == null) && (userProduct.UserId == 0 || userProduct.UserId == null))
            {
                var maxUserProductId = _dbContext.OrderProducts.Max(p => (int?)p.ProductId) ?? 0;
                var maxUserUserId = _dbContext.OrderProducts.Max(p => (int?)p.OrderId) ?? 0;
                userProduct.ProductId = maxUserProductId + 1;
                userProduct.UserId = maxUserUserId + 1;
            }

            _dbContext.Add(userProduct);
            _dbContext.SaveChanges();
        }
        public override void Delete(int id)
        {
            var userProduct = _dbContext.UserProducts.FirstOrDefault(x => x.UserId == id) ?? throw new Exception("Hatalı ID girişi: Kullanıcı/Ürün bulunamadı.");

            _dbContext.Remove(id);
            _dbContext.SaveChanges();

        }
        public override bool Update(UserProduct t)
        {
            var updatedUserProduct = GetById(t.UserId);
            if (updatedUserProduct == null)
            {
                return false;
            }
            return base.Update(updatedUserProduct);
        }
    }
}