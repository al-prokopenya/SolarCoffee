using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Services;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class ProductController: ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet("/api/product")]
        public ActionResult GetProduct()
        {
            _logger.LogInformation("Getting all products");
            var products = _productService.GetAllProducts();

            var productViewModels = products
                .Select(ProductMapper.SerializeToViewModel);

            return Ok(productViewModels);
        }

        /// <summary>
        /// Save new product to DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost("/api/product")]
        public ActionResult AddProduct([FromBody] ProductModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Adding new product");
            var newProduct = ProductMapper.SerializeToDataModel(product);

            var newProductResponse = _productService.CreateProduct(newProduct);

            if (newProductResponse.IsSuccess)
            {
                var newProductResponseView = ProductMapper.SerializeToViewModel(newProductResponse.Data);

                return Ok(newProductResponseView);
            }

            throw new Exception(newProductResponse.Message);
        }

        [HttpPatch("/api/product/{id}")]
        public ActionResult ArchiveProduct(int id)
        {
            _logger.LogInformation("Archiving the product");

            var result = _productService.ArchiveProduct(id);

            return Ok(result);
        }
    }
}