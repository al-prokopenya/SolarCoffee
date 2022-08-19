using SolarCoffee.Data.Models;
using System.Collections.Generic;

namespace SolarCoffee.Services
{
    public interface IOrderService
    {
        ServiceResponse<bool> GenerateOpenOrder(SalesOrder order);
        List<SalesOrder> GetOrders();
        ServiceResponse<bool> MarkFullfilled(int id);
    }
}