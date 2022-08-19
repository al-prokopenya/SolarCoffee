using System;
using System.Collections.Generic;
using System.Text;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services
{
    public interface IProductService
    {
        List<Product> GetAllProducts();

        Product GetProduct(int id);

        ServiceResponse<Product> CreateProduct(Product product);

        ServiceResponse<Product> ArchiveProduct(int id);
    }
}
