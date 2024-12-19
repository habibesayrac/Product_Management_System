using PMS.DataAccessLayer.Abstract;
using PMS.DataAccessLayer.Concrete;
using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly Context _dbContext;
        public ProductRepository(Context context) : base(context)
        {
            _dbContext = context;
        }
        public Product GetById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);
            return product != null ? product : null;
        }
        public override void Add(Product product)
        {
            if (product.ProductId == 0 || product.ProductId == null)
            {
                var maxProductId = _dbContext.Products.Max(p => (int?)p.ProductId) ?? 0;
                product.ProductId = maxProductId + 1;
            }

            _dbContext.Add(product);
            _dbContext.SaveChanges();
        }
        public override void Delete(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(x => x.ProductId == id);

            if (product == null)
            {
                throw new Exception("Hatalı ID girişi: Ürün bulunamadı.");
            }
            _dbContext.Remove(product);
            _dbContext.SaveChanges();
        }
        public override bool Update(Product t)
        {
            var updatedProduct = GetById(t.ProductId);
            if (updatedProduct == null)
            {
                return false;
            }
            updatedProduct.Name = t.Name;
            updatedProduct.Description = t.Description;
            updatedProduct.ModifiedDate = DateTime.Now;

            return base.Update(updatedProduct);
        }
        public bool MultipleUpdateProduct(List<Product> products)
        {
            foreach (var updatedProduct in products)
            {
                var existingProduct = products.FirstOrDefault(c => c.ProductId == updatedProduct.ProductId);

                if (existingProduct != null)
                {
                    Update(existingProduct);
                }
                throw new Exception("Ürün bulunamadı.");
            }
            return true;
        }
        public bool ReduceStock(int ProductId, int quantity)
        {
            var value = _dbContext.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (value == null)
            {
                throw new Exception("Ürün bulunamadı.");

            }
            if (value.Stock >= quantity)
            {
                value.Stock -= quantity;
                return true;
            }
            return false;
        }
        
    }
}