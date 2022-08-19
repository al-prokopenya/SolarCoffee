using SolarCoffee.Web.ViewModels;
using SolarCoffee.Data.Models;
namespace SolarCoffee.Web.Serialization
{
    public static class ProductMapper
    {
        /// <summary>
        /// Maps a Product data model to Product view model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static ProductModel SerializeToViewModel(Product product)
        {
            return new ProductModel
            {
                CreatedOn = product.CreatedOn,
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable,
                Name = product.Name,
                UpdatedOn = product.UpdatedOn
            };
        }

        /// <summary>
        /// Maps a Product view model to Product data model
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>

        public static Product SerializeToDataModel(ProductModel product)
        {
            return new Product
            {
                CreatedOn = product.CreatedOn,
                Description = product.Description,
                Id = product.Id,
                Price = product.Price,
                IsArchived = product.IsArchived,
                IsTaxable = product.IsTaxable,
                Name = product.Name,
                UpdatedOn = product.UpdatedOn
            };
        }
    }
}
