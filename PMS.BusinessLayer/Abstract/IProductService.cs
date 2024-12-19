using PMS.DTOLayer.ProductDto;
using PMS.EntityLayer;

namespace PMS.BusinessLayer.Abstract
{
    public interface IProductService
    {
        void Add(AddProductDto addProductDto);
        bool Update(UpdateProductDto updateProductDto);
        void Delete(int id);
        List<Product> GetList();
        public Product GetById(int id);
        public bool MultipleUpdateProduct(List<UpdateProductDto> updateProductDto);

    }
}