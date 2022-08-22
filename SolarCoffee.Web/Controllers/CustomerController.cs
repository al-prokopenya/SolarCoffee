using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Services;
using SolarCoffee.Web.Serialization;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers
{
    [ApiController]
    public class CustomerController: ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpGet("/api/customer")]
        public ActionResult GetCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            var customersModels = customers
                .Select(CustomerMapper.SerializeToView)
                .OrderByDescending(customer => customer.CreatedOn)
                .ToList();

            return Ok(customersModels);
        }

        [HttpDelete("/api/customer/{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            _logger.LogInformation($"Delete customer with id {id}");

            var res = _customerService.DeleteCustomer(id);

            return Ok(res);
        }

        [HttpGet("/api/customer/{id}")]
        public ActionResult GetCustomer(int id)
        {
            _logger.LogInformation($"Get customer with id {id}");

            var customer = _customerService.GetById(id);
            var customerModel = CustomerMapper.SerializeToView(customer);

            return Ok(customerModel);
        }


        [HttpPost("/api/customer")]
        public ActionResult CreateCustomer([FromBody]CustomerModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating new customer");
            customer.CreatedOn = customer.UpdatedOn = DateTime.UtcNow;

            var customerData = CustomerMapper.SerializeToData(customer);
            var res = _customerService.CreateCustomer(customerData);

            return Ok(res);
        }
    }
}
