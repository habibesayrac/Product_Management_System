using PMS.DataAccessLayer.ResponseModel;
using PMS.DTOLayer.CategoryDto;
using PMS.EntityLayer;

namespace PMS.BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        public BaseResponseModel Add(AddCategoryDto addCategoryDto);
        bool Update(UpdateCategoryDto updateCategoryDto);
        void Delete(int id);
        List<Category> GetList();
        public Category GetById(int id);
        bool MultipleUpdateCategory(List<UpdateCategoryDto> updateCategoryDto);
 
    }
}