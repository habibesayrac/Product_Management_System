using PMS.DataAccessLayer.Abstract;
using PMS.DataAccessLayer.Concrete;
using PMS.EntityLayer;

namespace PMS.DataAccessLayer.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly Context _dbContext;
        public CategoryRepository(Context context) : base(context)
        {
            _dbContext = context;
        }
        public Category GetById(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == id);
            return category != null ? category : null;
        }
        public override void Delete(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(x => x.CategoryId == id);

            if (category == null)
            {
                throw new Exception("Hatalı ID girişi: Kategori bulunamadı.");
            }
            _dbContext.Remove(category);
            _dbContext.SaveChanges();
        }
        public override bool Update(Category t)
        {
            var updatedCategory = GetById(t.CategoryId);
            if (updatedCategory == null)
            {
                return false;
            }
            updatedCategory.Name = t.Name;
            updatedCategory.Description = t.Description;
            updatedCategory.ModifiedDate = DateTime.Now;

            return base.Update(updatedCategory);
        }

        public bool MultipleUpdateCategory(List<Category> category)
        {
            foreach (var updatedCategory in category)
            {
                var existingCategory = category.FirstOrDefault(c => c.CategoryId == updatedCategory.CategoryId);

                if (existingCategory != null && category.Any(c => c.CategoryId == existingCategory.CategoryId))
                {
                    Update(existingCategory);
                }
                throw new Exception("Kategori bulunamadı.");
            }
            return true;
        }
    }
}