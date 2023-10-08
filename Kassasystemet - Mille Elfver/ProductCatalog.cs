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
            DataSeeding();

            //LoadProductsFromFile();
        }

        /// <summary>
        /// Loads all products and saves them to Produkter.txt
        /// </summary>
        private void DataSeeding()
        {
            if (availableProducts.Count == 0)
            {
                availableProducts.Add("300", new Product("300", "Bananer", 15.50m, 15.50m));
                availableProducts.Add("301", new Product("301", "Tomater", 79.90m, 79.90m));
                availableProducts.Add("302", new Product("302", "Apelsiner", 9.90m, 9.90m));
                availableProducts.Add("303", new Product("303", "Godis", 99.50m, 99.50m));
                availableProducts.Add("304", new Product("304", "Kött", 199m, 199m));
                availableProducts.Add("305", new Product("305", "Kyckling", 129.90m, 129.90m));
                availableProducts.Add("306", new Product("306", "Nutella", 21.90m, 0));
                availableProducts.Add("307", new Product("307", "Citron", 5.50m, 0));
                availableProducts.Add("308", new Product("308", "Jordgubbar", 39.90m, 0));
                availableProducts.Add("309", new Product("309", "Grädde", 24.90m, 0));
                availableProducts.Add("310", new Product("310", "Choklad", 22.90m, 0));
                availableProducts.Add("311", new Product("311", "Mango", 19.90m, 0));
                availableProducts.Add("312", new Product("312", "Öl", 15.50m, 0));
                availableProducts.Add("313", new Product("313", "Kex", 23.90m, 0));
                availableProducts.Add("314", new Product("314", "Gurka", 10m, 0));
                availableProducts.Add("315", new Product("315", "Leverpastej", 24.90m, 0));
                availableProducts.Add("316", new Product("316", "Chips", 27.90m, 0));
                availableProducts.Add("317", new Product("317", "Bröd", 34.90m, 0));
                availableProducts.Add("318", new Product("318", "Mjölk", 16.90m, 0));
                availableProducts.Add("319", new Product("319", "Yoghurt", 29.90m, 0));
                availableProducts.Add("320", new Product("320", "Glass", 39.90m, 0));

                SaveProductsToFile();
            }
        }

        /// <summary>
        /// Saves all products to Produkter.txt file
        /// </summary>
        private void SaveProductsToFile()
        {
            try
            {
                Directory.CreateDirectory($"../../../Produkter");
                using (StreamWriter writer = new StreamWriter("../../../Produkter/Produkter.txt"))
                {
                    foreach (var product in availableProducts.Values)
                    {
                        writer.WriteLine($"{product.Id}, {product.Name}, {product.UnitPrice}, {product.KiloPrice}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparandet av produktlistan: {ex.Message}");
            }
        }

        //todo ändra denna metod då logiken är brusten, skriver enbart ut kilopriser
        //public void LoadProductsFromFile()
        //{
        //    if (File.Exists("../../../Produkter/Produkter.txt"))
        //    {
        //        using (StreamReader reader = new StreamReader("../../../Produkter/Produkter.txt"))
        //        {
        //            string line;
        //            while ((line = reader.ReadLine()) != null)
        //            {
        //                string[] parts = line.Split(',');
        //                if (parts.Length == 5)
        //                {
        //                    string id = parts[0].Trim();
        //                    string name = parts[1].Trim();
        //                    decimal unitPrice = decimal.Parse(parts[2].Trim());
        //                    decimal kiloPrice = decimal.Parse(parts[3].Trim());

        //                    availableProducts[id] = new Product(id, name, unitPrice, kiloPrice);
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Checks if productId input exists (example 300-320) otherwise returns null
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
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
            Console.WriteLine("Tillgängliga produkter:");
            Console.WriteLine($"300-305 är kilopris, 306-320 är styckpris");
            foreach (var product in availableProducts)
            {
                Console.WriteLine($"ProduktID: {product.Key}: {product.Value.Name} Pris: {product.Value.UnitPrice}");
            }
        }
    }
}
