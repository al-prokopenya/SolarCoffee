using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarCoffee.Data.Models;
using SolarCoffee.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SolarCoffee.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly SolarDbContext _db;
        private readonly ILogger<InventoryService> _logger;

        public InventoryService(SolarDbContext db, ILogger<InventoryService> logger)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get current inventory from db
        /// </summary>
        /// <returns></returns>
        public List<ProductInventory> GetCurrentInventory()
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .Where(pi => !pi.Product.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Updates number of units available of the provided product id
        /// Adjust QuantityOnHand by adjustment value
        /// </summary>
        /// <param name="productId">product Id</param>
        /// <param name="adjustment">number of units added/removed</param>
        /// <returns></returns>
        public ServiceResponse<ProductInventory> UpdateUnitsAvailable(int productId, int adjustment)
        {
            try
            {
                var inventory = _db.ProductInventories
                    .Include(pi => pi.Product)
                    .First(inv => inv.Product.Id == productId);

                inventory.QuantityOnHand += adjustment;

                try
                {
                    CreateSnapshot(inventory);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error creating inventory snapshot\n{ex.StackTrace}");
                }

                _db.SaveChanges();

                return new ServiceResponse<ProductInventory>
                {
                    Data = inventory,
                    IsSuccess = true,
                    Message = $"Product {productId} inventory adjusted",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ProductInventory>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = $"Error updating ProductInventory QuantityOnHand\n{ex.StackTrace}",
                };
            }
        }

        /// <summary>
        /// Gets a ProductInventory instance by Product ID
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>

        public ProductInventory GetByProductId(int productId)
        {
            return _db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
        }

        /// <summary>
        /// Get snapshot history for previous 6 hours
        /// </summary>
        /// <returns></returns>
        public List<ProductInventorySnapshot> GetSnapshotHistory()
        {
            var earliest = DateTime.UtcNow.AddHours(-6);

            return _db.ProductInventorySnapshots
                .Include(snap => snap.Product)
                .Where(snap => snap.SnapshotTime > earliest
                                && !snap.Product.IsArchived)
                .ToList();
        }

        /// <summary>
        /// Creates a snapshot record using the provided product inventory instance
        /// </summary>
        private void CreateSnapshot(ProductInventory inventory)
        {
            var snapshot = new ProductInventorySnapshot
            {
                SnapshotTime = DateTime.UtcNow,
                Product = inventory.Product,
                QuantityOnHand = inventory.QuantityOnHand
            };

            _db.Add(snapshot);
        }
    }
}
