using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolarCoffee.Data.Models;
using SolarCoffee.Data;
using Microsoft.EntityFrameworkCore;

namespace SolarCoffee.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly SolarDbContext _db;
        public CustomerService(SolarDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        /// <summary>
        /// Returns the list of customers from db
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllCustomers()
        {
            return _db.Customers
                .Include(customer => customer.PrimaryAddress)
                .OrderBy(customer => customer.LastName)
                .ToList();
        }

        /// <summary>
        /// Gets the customer record by primary key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetById(int id)
        {
            return _db.Customers.Find(id);
        }
        /// <summary>
        /// Deletes customer record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public ServiceResponse<bool> DeleteCustomer(int id)
        {
            var customer = _db.Customers.Find(id);

            if (customer == null)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = "Customer to delete not found"
                };
            }

            try
            {
                _db.Customers.Remove(customer);
                _db.SaveChanges();

                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = true,
                    Message = "Customer deleted"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    IsSuccess = false,
                    Data = false,
                    Message = ex.StackTrace
                };
            }

        }

        /// <summary>
        /// Add new Customer record
        /// </summary>
        /// <param name="customer">Customer instance</param>
        /// <returns>ServiceResponse<Customer></returns>
        public ServiceResponse<Customer> CreateCustomer(Customer customer)
        {
            try
            {
                _db.Customers.Add(customer);
                //   _db.CustomerAddresses.Add(customer.PrimaryAddress);
                _db.SaveChanges();

                return new ServiceResponse<Customer>()
                {
                    Data = customer,
                    IsSuccess = true,
                    Message = "Customer saved"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Customer>()
                {
                    Data = customer,
                    IsSuccess = false,
                    Message = ex.StackTrace
                };
            }
        }
    }
}
