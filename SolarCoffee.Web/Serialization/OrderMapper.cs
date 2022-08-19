using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;
namespace SolarCoffee.Web.Serialization
{
    public static class OrderMapper
    {
        /// <summary>
        /// serializes invoice model to sales order data object
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static SalesOrder SerializeToSalesOrder(InvoiceModel invoice)
        {
            var salesOrderItems = invoice.LineItems.Select(item => new SalesOrderItem
            {
                Id = item.Id,
                Product = ProductMapper.SerializeToDataModel(item.Product),
                Quantity = item.Quantity
            }).ToList();

            return new SalesOrder()
            {
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                //IsPaid = invoice.IsPaid,
                //Id = invoice.Id,
                SalesOrderItems = salesOrderItems,
            };
        }

        public static List<OrderModel> SerializeOrdersToViewModel(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderModel() { 
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                Id = order.Id,
                IsPaid = order.IsPaid,
                Customer = CustomerMapper.SerializeToView(order.Customer),
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems)
            }).ToList();
        }

        /// <summary>
        /// Maps collection sales order item data objects to collection of views
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private static List<SalesOrderItemModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItem> items)
        { 
            return items.Select(item => new SalesOrderItemModel { 
                Id = item.Id,
                Product = ProductMapper.SerializeToViewModel(item.Product),
                Quantity = item.Quantity
            }).ToList();
        }
    }
}
