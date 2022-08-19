using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarCoffee.Services
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext _db;

        public ProductService(SolarDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        /// <summary>
        /// ARchives a product by setting IsArchived property to true
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<Product> ArchiveProduct(int id)
        {
            var product = GetProduct(id);

            if (product != null)
            {
                product.IsArchived = true;
                _db.SaveChanges();

                return new ServiceResponse<Product> { 
                    IsSuccess = true,
                    Data = product,
                    Message = "Product archived"
                };
            }

            return new ServiceResponse<Product>
            {
                IsSuccess = false,
                Message = "Product not found"
            };
        }

        /// <summary>
        /// Adds new product to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ServiceResponse<Product> CreateProduct(Product product)
        {
            try
            {
                _db.Products.Add(product);

                var newInventory = new ProductInventory
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10
                };

                _db.ProductInventories.Add(newInventory);

                _db.SaveChanges();

                return new ServiceResponse<Product> {
                    Data = product,
                    IsSuccess = true,
                    Message = "Saved new product"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Product> { 
                    Data = product,
                    IsSuccess = false,
                    Message = ex.StackTrace
                };
            }
        }

        /// <summary>
        /// Retrieves all products from database
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
           return _db.Products.ToList();
        }

        /// <summary>
        /// Retrieves a product by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            return _db.Products.Find(id);
        }
    }
}
