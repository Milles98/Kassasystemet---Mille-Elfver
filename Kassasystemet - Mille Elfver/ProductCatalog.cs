using Kassasystemet___Mille_Elfver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ProductCatalog
    {
        private readonly Dictionary<string, Product> availableProducts = new Dictionary<string, Product>();

        public ProductCatalog()
        {
            availableProducts.Add("300", new Product("300", "Bananer", 15.50m, 15.50m));
            availableProducts.Add("301", new Product("301", "Nutella", 21.90m, 0));
            availableProducts.Add("302", new Product("302", "Citron", 5.50m, 0));
            availableProducts.Add("303", new Product("303", "Jordgubbar", 39.90m, 0));
            availableProducts.Add("304", new Product("304", "Grädde", 24.90m, 0));
            availableProducts.Add("305", new Product("305", "Choklad", 22.90m, 0));
            availableProducts.Add("306", new Product("306", "Apelsiner", 9.90m, 9.90m));
            availableProducts.Add("307", new Product("307", "Mango", 19.90m, 0));
            availableProducts.Add("308", new Product("308", "Tomater", 79.90m, 79.90m));
            availableProducts.Add("309", new Product("309", "Kött", 199m, 199m));
            availableProducts.Add("310", new Product("310", "Godis", 99.50m, 99.50m));
        }

        public Product GetProduct(string productId)
        {
            if (availableProducts.TryGetValue(productId, out var product))
            {
                return product;
            }
            return null;
        }

        public void DisplayAvailableProducts()
        {
            Console.Clear();
            Console.WriteLine("Tillgängliga produkter:");
            foreach (var product in availableProducts)
            {
                Console.WriteLine($"ProduktID: {product.Key}: {product.Value.Name} Pris: {product.Value.UnitPrice} Pristyp:{product.Value.KiloPrice} (0 = st, över 0 = kilopris)\n");
            }
        }
    }
}
