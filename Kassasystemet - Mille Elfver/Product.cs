using Kassasystemet___Mille_Elfver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Product
    {

        //auto implemented properties (typ som instansvariabler men fixade i bakgrunden)
        //todo göra om dom till getters och setters med villkor
        public string Name { get; }
        public decimal UnitPrice { get; }
        public decimal KiloPrice { get; }
        public bool IsKiloPrice { get; }

        //name är ex bananer, price ex 15.50m, priceType ex kg
        //konstruktor
        public Product(string name, decimal unitPrice, decimal kiloPrice)
        {
            Name = name;
            UnitPrice = unitPrice;
            KiloPrice = kiloPrice;
            IsKiloPrice = kiloPrice > 0;
        }

        public static readonly Dictionary<string, Product> availableProducts = new Dictionary<string, Product>
                {
            //sista siffrorna = om det är över 0 blir det kilopris, om det är 0 = styckpris
            { "300", new Product("Bananer", 15.50m, 15.50m) },
            { "301", new Product("Nutella", 21.90m, 0) },
            { "302", new Product("Citron", 5.50m, 0) },
            { "303", new Product("Jordgubbar", 39.90m, 0) },
            { "304", new Product("Grädde", 24.90m, 0) },
            { "305", new Product("Choklad", 22.90m, 0) },
            { "306", new Product("Apelsiner", 9.90m, 9.90m) },
            { "307", new Product("Mango", 19.90m, 0) },
            { "308", new Product("Tomater", 79.90m, 79.90m) },
            { "309", new Product("Kött", 199m, 199m) },
            { "310", new Product("Godis", 99.50m, 99.50m) }
                };

        /// <summary>
        /// Shows available products in dictionary to user
        /// </summary>
        public static void DisplayTheProducts()
        {
            Console.Clear();
            Console.WriteLine("Tillgängliga produkter:");
            foreach (var product in Product.availableProducts)
            {
                Console.WriteLine($"{product.Key}:{product.Value.Name}({product.Value.UnitPrice} {product.Value.KiloPrice})\n");
                //Console.WriteLine($"{product.Key}: {product.Value.Name} ({product.Value.Price} {product.Value.PriceType})\n");
            }
        }
    }
}
