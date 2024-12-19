using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Abstract
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        public Product GetById(int id);
        public bool MultipleUpdateProduct(List<Product> products);
        public bool ReduceStock(int ProductId, int quantity);

    }
}           