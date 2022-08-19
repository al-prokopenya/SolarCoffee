using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarCoffee.Data.Models;
using SolarCoffee.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace SolarCoffee.Services
{
    public class OrderService : IOrderService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public OrderService(ILogger<OrderService> logger,
            SolarDbContext dbContext,
            IProductService productService,
            IInventoryService inventoryService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
        }

        /// <summary>
        /// Gets all Sales orders from the system
        /// </summary>
        /// <returns></returns>
        public List<SalesOrder> GetOrders()
        {
            return _db.SalesOrders
                .Include(order => order.SalesOrderItems)
                    .ThenInclude(item => item.Product)
                .Include(order => order.Customer)
                    .ThenInclude(customer => customer.PrimaryAddress)
                .ToList();
        }

        /// <summary>
        /// Creates an OpenSales order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResponse<bool> GenerateOpenOrder(SalesOrder order)
        {
            _logger.LogInformation("Generating new order");

            foreach (var item in order.SalesOrderItems)
            {
                item.Product = _productService
                    .GetProduct(item.Product.Id);

                var inventoryId = _inventoryService
                    .GetByProductId(item.Product.Id).Id;

                _inventoryService
                    .UpdateUnitsAvailable(inventoryId, -item.Quantity);
            }

            try
            {
                _db.SalesOrders.Add(order);
                _db.SaveChanges();

                _logger.LogInformation($"Order generated {order.Id}");

                return new ServiceResponse<bool>
                {
                    IsSuccess = true,
                    Data = true,
                    Message = "Order created"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = $"Error creating order\n{ex.StackTrace}"
                };
            }
        }

        /// <summary>
        /// Marks an open order as paid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServiceResponse<bool> MarkFullfilled(int id)
        {
            var order = _db.SalesOrders.Find(id);

            if (order == null)
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = $"Order {id} not found"
                };

            try
            {
                order.IsPaid = true;
                order.UpdatedOn = DateTime.UtcNow;
                _db.SalesOrders.Update(order);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    Data = true,
                    IsSuccess = true,
                    Message = $"Order {id} marked as fullfiled"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = $"Error updating order status\n{ex.StackTrace}"
                };
            }
        }
    }
}
