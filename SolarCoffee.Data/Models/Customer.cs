using System;
using System.ComponentModel.DataAnnotations;
namespace SolarCoffee.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public CustomerAddress PrimaryAddress { get; set; }
    }
}