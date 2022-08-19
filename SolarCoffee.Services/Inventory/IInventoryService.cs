using SolarCoffee.Data.Models;
using System.Collections.Generic;

namespace SolarCoffee.Services
{
    public interface IInventoryService
    {
        ProductInventory GetByProductId(int productId);
        List<ProductInventory> GetCurrentInventory();
        List<ProductInventorySnapshot> GetSnapshotHistory();
        ServiceResponse<ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
    }
}