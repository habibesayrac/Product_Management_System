using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Abstract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Category GetById(int id);
        public bool MultipleUpdateCategory(List<Category> category);
    }
}