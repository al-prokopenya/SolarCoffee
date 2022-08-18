using System;

namespace SolarCoffee.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public string LastName { get; set; }

        public CustomerAddress PrimaryAddress { get; set; }
    }
}