﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal KiloPrice { get; set; }
        public bool IsKiloPrice { get; }
        public ProductDiscount Discounts { get; set; }

        public Product(string id, string name, decimal unitPrice, decimal kiloPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            KiloPrice = kiloPrice;
            IsKiloPrice = kiloPrice > 0;

            Discounts = new ProductDiscount(0, DateTime.MinValue, DateTime.MinValue);
        }
    }
}
