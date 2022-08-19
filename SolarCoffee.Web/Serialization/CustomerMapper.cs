using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serialization
{
    public static class CustomerMapper
    {
        public static CustomerModel SerializeToView(Customer customer)
        {
            return new CustomerModel
            {
                Id = customer.Id, 
                UpdatedOn = customer.UpdatedOn,
                CreatedOn = customer.CreatedOn,
                LastName = customer.LastName,
                Name = customer.Name,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        public static Customer SerializeToData(CustomerModel customer)
        {
            return new () 
            {
                Id = customer.Id,
                UpdatedOn = customer.UpdatedOn,
                CreatedOn = customer.CreatedOn,
                LastName = customer.LastName,
                Name = customer.Name,
                PrimaryAddress = MapCustomerAddress(customer.PrimaryAddress)
            };
        }

        public static CustomerAddressModel MapCustomerAddress(CustomerAddress address)
        {
            return new() 
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                Country = address.Country,
                CreatedOn = address.CreatedOn,
                Id = address.Id,
                State= address.State,
                UpdatedOn = address.UpdatedOn,
                ZipCode = address.ZipCode
            };
        }

        public static CustomerAddress MapCustomerAddress(CustomerAddressModel address)
        {
            return new()
            {
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                City = address.City,
                Country = address.Country,
                CreatedOn = address.CreatedOn,
                Id = address.Id,
                State = address.State,
                UpdatedOn = address.UpdatedOn,
                ZipCode = address.ZipCode
            };
        }
    }
}
