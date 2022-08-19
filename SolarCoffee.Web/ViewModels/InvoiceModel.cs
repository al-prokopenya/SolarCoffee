namespace SolarCoffee.Web.ViewModels
{
    /// <summary>
    /// View model for SalesOrders
    /// </summary>
    public class InvoiceModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int CustomerId { get; set; }

        public List<SalesOrderItemModel> LineItems { get; set; }

        public bool IsPaid { get; set; }
    }

    public class SalesOrderItemModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public ProductModel Product { get; set; }
    }
}
