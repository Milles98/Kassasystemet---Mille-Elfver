using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Product
    {
        public static Dictionary<string, Product> availableProducts = new Dictionary<string, Product>
        {
            { "300", new Product("Bananer", 15.50m, "st") },
            { "301", new Product("Nutella", 21.90m, "st") },
            { "302", new Product("Citron", 5.50m, "st") },
            { "303", new Product("Jordgubbar", 39.90m, "st") },
            { "304", new Product("Grädde", 24.90m, "st") },
            { "305", new Product("Choklad", 22.90m, "st") },
            { "306", new Product("Apelsiner", 10, "st") },
            { "307", new Product("Mango", 20, "st") },
            { "308", new Product("Tomater", 49.90m, "st") },
            { "309", new Product("Kött", 229.90m, "st") },
            { "310", new Product("Godis", 99.50m, "st") }
        };

        public string Name { get; }
        public decimal UnitPrice { get; }
        public string PriceType { get; }

        public Product(string name, decimal unitPrice, string priceType)
        {
            Name = name;
            UnitPrice = unitPrice;
            PriceType = priceType;
        }

        public static void DisplayTheProducts()
        {
            Console.Clear();
            Console.WriteLine("Tillgängliga produkter:");
            foreach (var product in Product.availableProducts)
            {
                Console.WriteLine($"{product.Key}: {product.Value.Name} ({product.Value.UnitPrice} {product.Value.PriceType})\n");
            }
        }
    }
}
