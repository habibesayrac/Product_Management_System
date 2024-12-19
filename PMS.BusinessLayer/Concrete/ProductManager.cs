using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PMS.BusinessLayer.Abstract;
using PMS.DataAccessLayer.Abstract;
using PMS.DTOLayer.ProductDto;
using PMS.EntityLayer;
using PMS.RabbitMq.RabbitMq;
using PMS.Redis.Repository;

namespace PMS.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRedisRepository _redisRepository;
        private readonly IRabbitMqService _rabbitMqService;
        private readonly ILogger<ProductManager> _logger;
        public ProductManager(IProductRepository productRepository, IRedisRepository redisRepository, IRabbitMqService rabbitMqService, ILogger<ProductManager> logger)
        {
            _productRepository = productRepository;
            _redisRepository = redisRepository;
            _rabbitMqService = rabbitMqService;
            _logger = logger;
        }

        public void Add(AddProductDto addProductDto)
        {
            Product product = new Product()
            {
                Name = addProductDto.Name,
                Description = addProductDto.Description,
                CreatedDate = DateTime.Now,
                CategoryId = addProductDto.CategoryId
            };

            string message = JsonConvert.SerializeObject(product);
            _rabbitMqService.PublishMessage("productmesajkuyrugu", message);
            _logger.LogInformation("Product loglanmak üzere alındı.");

            _productRepository.Add(product);
        }

        public void Delete(int id)
        {
            string message = JsonConvert.SerializeObject(id);
            _rabbitMqService.PublishMessage("productmesajkuyrugu", message);
            _logger.LogInformation("Product silinmek üzere alındı.");

            _productRepository.Delete(id);
        }

        public List<Product> GetList()
        {
            var key = "productkey";

            var cacheResult = _redisRepository.GetData<List<Product>>(key);
            if (cacheResult == null)
            {
                cacheResult = _productRepository.GetList();
            }
            _redisRepository.SetData<List<Product>>(key, cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;
        }

        public Product GetById(int id)
        {
            var cacheResult = _redisRepository.GetData<Product>(id.ToString());
            if (cacheResult == null)
            {
                cacheResult = _productRepository.GetById(id);

            }
            _redisRepository.SetData<Product>(id.ToString(), cacheResult, TimeSpan.FromMinutes(10));
            return cacheResult;
        }
        public bool Update(UpdateProductDto updateProductDto)
        {
            var updatedProduct = GetById(updateProductDto.ProductId);
            if (updatedProduct == null)
            {
                return false;
            }
            updatedProduct.Name = updateProductDto.Name;
            updatedProduct.Description = updateProductDto.Description;
            updatedProduct.ModifiedDate = DateTime.Now;

            string message = JsonConvert.SerializeObject(updatedProduct);
            _rabbitMqService.PublishMessage("productmesajkuyrugu", message);
            _logger.LogInformation("Product güncelleme loglanmak üzere alındı.");


            return _productRepository.Update(updatedProduct);
        }
        public bool MultipleUpdateProduct(List<UpdateProductDto> updateProductDto)
        {
            foreach (var updatedProduct in updateProductDto)
            {
                var existingCategory = updateProductDto.FirstOrDefault(c => c.ProductId == updatedProduct.ProductId);
                var prod = _productRepository.GetById(updatedProduct.ProductId);
                if (prod != null)
                {
                    Update(updatedProduct);
                }
                else
                {
                    throw new Exception("Ürün bulunamadı.");
                }
            }
            return true;
        }
    }
}