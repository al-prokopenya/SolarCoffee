﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SolarCoffee.Data.Models
{
    public class ProductInventory
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int QuantityOnHand { get; set; }

        public int IdealQuantity { get; set; }

        public Product Product { get; set; }
    }
}
