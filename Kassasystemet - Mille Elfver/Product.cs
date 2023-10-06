using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Product
    {
        public string Id { get; }
        public string Name { get; }
        public decimal UnitPrice { get; }
        public decimal KiloPrice { get; }
        public bool IsKiloPrice { get; }

        public Product(string id, string name, decimal unitPrice, decimal kiloPrice)
        {
            Id = id;
            Name = name;
            UnitPrice = unitPrice;
            KiloPrice = kiloPrice;
            IsKiloPrice = kiloPrice > 0;
        }
    }
}
