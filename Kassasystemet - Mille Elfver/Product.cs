using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    class Product
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; set; }

        public Product(string name, decimal unitPrice, string priceType)
        {
            Name = name;
            UnitPrice = unitPrice;
            PriceType = priceType;
        }
    }
}
