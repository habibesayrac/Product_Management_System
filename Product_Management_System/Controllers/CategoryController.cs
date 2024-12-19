using Microsoft.AspNetCore.Mvc;
using PMS.BusinessLayer.Abstract;
using PMS.DTOLayer.CategoryDto;

namespace Product_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }

        [HttpGet]
        public IActionResult GetListCategory()
        {
            var result = _categoryService.GetList();
            return Ok(result);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var result = _categoryService.GetById(id);

            if (result == null)
            {
                throw new Exception("Hatalı ID girişi: Kategori bulunamadı.");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddCategory(AddCategoryDto addCategoryDto)
        {
            _categoryService.Add(addCategoryDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            _categoryService.Delete(id);

            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            _categoryService.Update(updateCategoryDto);

            return Ok();
        }

        [HttpPut("multi")]
        public IActionResult MultipleUpdateCategory(List<UpdateCategoryDto> updateCategoryDto)
        {
            var value = _categoryService.MultipleUpdateCategory(updateCategoryDto);
            if (value)
            {
                return Ok();
            }
            return BadRequest("Kategori Bulunamadı");
        }
    }
}