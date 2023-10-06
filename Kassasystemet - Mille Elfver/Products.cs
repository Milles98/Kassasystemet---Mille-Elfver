using Kassasystemet___Mille_Elfver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Products
    {

        //todo göra om dom till getters och setters med villkor
        public string Name { get; }
        public decimal UnitPrice { get; }
        public decimal KiloPrice { get; }
        public bool IsKiloPrice { get; }

        public Products(string name, decimal unitPrice, decimal kiloPrice)
        {
            Name = name;
            UnitPrice = unitPrice;
            KiloPrice = kiloPrice;
            IsKiloPrice = kiloPrice > 0;
        }

        public static readonly Dictionary<string, Products> availableProducts = new Dictionary<string, Products>
                {
            { "300", new Products("Bananer", 15.50m, 15.50m) },
            { "301", new Products("Nutella", 21.90m, 0) },
            { "302", new Products("Citron", 5.50m, 0) },
            { "303", new Products("Jordgubbar", 39.90m, 0) },
            { "304", new Products("Grädde", 24.90m, 0) },
            { "305", new Products("Choklad", 22.90m, 0) },
            { "306", new Products("Apelsiner", 9.90m, 9.90m) },
            { "307", new Products("Mango", 19.90m, 0) },
            { "308", new Products("Tomater", 79.90m, 79.90m) },
            { "309", new Products("Kött", 199m, 199m) },
            { "310", new Products("Godis", 99.50m, 99.50m) }
                };

        /// <summary>
        /// Shows available products in dictionary to user
        /// </summary>
        public static void DisplayTheProducts()
        {
            Console.Clear();
            Console.WriteLine("Tillgängliga produkter:");
            foreach (var product in Products.availableProducts)
            {
                Console.WriteLine($"{product.Key}:{product.Value.Name}({product.Value.UnitPrice} {product.Value.KiloPrice})\n");
            }
        }
    }
}
