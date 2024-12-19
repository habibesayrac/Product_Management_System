using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMS.BusinessLayer.Abstract;
using PMS.DataAccessLayer.Abstract;
using PMS.DataAccessLayer.ResponseModel;
using PMS.DTOLayer.CategoryDto;
using PMS.EntityLayer;
using PMS.RabbitMq.RabbitMq;
using PMS.Redis.Repository;

namespace PMS.BusinessLayer.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly ILogger<CategoryManager> _logger;
        //private readonly IBaseResponseModel _baseResponseModel;

        public CategoryManager(ICategoryRepository categoryRepository, IRedisRepository redisRepository, IRabbitMqService rabbitMqService, ILogger<CategoryManager> logger)
        {
            _categoryRepository = categoryRepository;
            _redisRepository = redisRepository;
            _rabbitMqService = rabbitMqService;
            _logger = logger;
            _logger.LogInformation("Category Log ile çağrıldı.");
        }
        public BaseResponseModel Add(AddCategoryDto addCategoryDto)
        {
            Category category = new Category();

            category.Name = addCategoryDto.Name;
            category.Description = addCategoryDto.Description;
            category.CreatedDate = DateTime.Now;

            string message = JsonConvert.SerializeObject(category);

            _rabbitMqService.PublishMessage("categorymesajkuyrugu", message);
            _logger.LogInformation("Category loglanmak üzere alındı.");

            var value = _categoryRepository.Add(category);

            if (value)
            {
                return new SuccessResponseModel<string>();
            }
            return new ErrorResponseModel(401, false, "category ekleme başarısız.");
        }

        public void Delete(int id)
        {
            string message = JsonConvert.SerializeObject(id);

            _rabbitMqService.PublishMessage("categorymesajkuyrugu", message);
            _logger.LogInformation("Category silinmesi için loglandı.");
            _categoryRepository.Delete(id);
        }

        public List<Category> GetList()
        {
            var key = "categorykey";

            var cacheResult = _redisRepository.GetData<List<Category>>(key);
            if (cacheResult == null)
            {
                cacheResult = _categoryRepository.GetList();
            }

            _redisRepository.SetData<List<Category>>(key, cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;
        }
        public Category GetById(int id)
        {
            var cacheResult = _redisRepository.GetData<Category>(id.ToString());
            if (cacheResult == null)
            {
                cacheResult = _categoryRepository.GetById(id);
            }

            _redisRepository.SetData<Category>(id.ToString(), cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;
        }
        public bool Update(UpdateCategoryDto updateCategoryDto)
        {
            var updatedCategory = GetById(updateCategoryDto.CategoryId);
            if (updatedCategory == null)
            {
                return false;
            }
            updatedCategory.Name = updateCategoryDto.Name;
            updatedCategory.Description = updateCategoryDto.Description;
            updatedCategory.ModifiedDate = DateTime.Now;

            string message = JsonConvert.SerializeObject(updatedCategory);

            _rabbitMqService.PublishMessage("categorymesajkuyrugu", message);
            _logger.LogInformation("Category güncellenmek üzere loglandı.");

            return _categoryRepository.Update(updatedCategory);
        }

        public bool MultipleUpdateCategory(List<UpdateCategoryDto> updateCategoryDto)
        {
            foreach (var updatedCategory in updateCategoryDto)
            {
                var existingCategory = updateCategoryDto.FirstOrDefault(c => c.CategoryId == updatedCategory.CategoryId);
                var cat = _categoryRepository.GetById(updatedCategory.CategoryId);
                if (cat != null)
                {
                    Update(updatedCategory);
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}