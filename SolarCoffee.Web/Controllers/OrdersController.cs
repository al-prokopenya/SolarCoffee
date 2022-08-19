using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Data;
using SolarCoffee.Services;
using SolarCoffee.Web.ViewModels;
using SolarCoffee.Web.Serialization;

namespace SolarCoffee.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;

        public OrdersController(ILogger<OrdersController> logger, IOrderService orderService, ICustomerService customerService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpPost("/api/invoice")]
        public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice) 
        {
            _logger.LogInformation("Generating invoice");

            var order = OrderMapper.SerializeToSalesOrder(invoice);
            order.Customer = _customerService.GetById(invoice.CustomerId);

            _orderService.GenerateOpenOrder(order);

            return Ok();
        }

        [HttpGet("/api/order")]
        public ActionResult GetOrders()
        {
            var orders = _orderService.GetOrders();
            var orderModels = OrderMapper.SerializeOrdersToViewModel(orders);

            return Ok(orderModels);
        }

        [HttpPatch("/api/order/complete/{id}")]
        public ActionResult CompleteOrder(int id)
        {
            _logger.LogInformation($"Making order {id} complete...");

            var res = _orderService.MarkFullfilled(id);

            return Ok(res);
        }
    }
}
