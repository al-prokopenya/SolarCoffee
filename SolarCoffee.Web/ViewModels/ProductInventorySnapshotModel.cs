namespace SolarCoffee.Web.ViewModels
{
    public class ProductInventorySnapshotModel
    {
        public int ProductId { get; set; }
        public List<int> QuantityOnHand { get; set; }
    }

    public class SnaphotResponse { 
        public List<ProductInventorySnapshotModel>
            ProductInventorySnapshots { get; set; }
        public List<DateTime> TimeLine { get; set; }
    }
}
