using SolarCoffee.Data.Models;
using System.Collections.Generic;

namespace SolarCoffee.Services
{
    public interface ICustomerService
    {
        ServiceResponse<Customer> CreateCustomer(Customer customer);
        ServiceResponse<bool> DeleteCustomer(int id);
        List<Customer> GetAllCustomers();
        Customer GetById(int id);
    }
}