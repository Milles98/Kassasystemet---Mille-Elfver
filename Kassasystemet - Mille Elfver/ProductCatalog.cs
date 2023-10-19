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
        private Dictionary<string, Product> availableProducts = new Dictionary<string, Product>();

        public ProductCatalog()
        {
            DataSeeding();
        }

        /// <summary>
        /// Loads all products and saves them to Produkter.txt
        /// </summary>
        public void DataSeeding()
        {
            LoadProductsFromFile();
            if (availableProducts.Count <= 20)
            {
                StartingItems();
                SaveProductsToFile();
            }
        }

        private void StartingItems()
        {
            //todo ta bort onödiga id från höger sida och lite refactoring
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
        }

        public void SetDiscount(string productId, decimal discount, DateTime startDate, DateTime endDate)
        {
            if (availableProducts.ContainsKey(productId))
            {
                Product product = availableProducts[productId];
                product.Discount = discount;
                product.DiscountStartDate = startDate;
                product.DiscountEndDate = endDate;
                SaveProductsToFile();
                Console.WriteLine($"Kampanjpris satt på produkt med ID {productId}.");
            }
            else
            {
                Console.WriteLine($"Produkt med ID {productId} finns ej.");
            }
        }

        public void RemoveDiscount(string productId)
        {
            if (availableProducts.ContainsKey(productId))
            {
                availableProducts[productId].Discount = 0;
                availableProducts[productId].DiscountStartDate = DateTime.MinValue;
                availableProducts[productId].DiscountEndDate = DateTime.MinValue;
                SaveProductsToFile();
                Console.WriteLine($"Rabatt har tagits bort för produkt med ID {productId}.");
            }
            else
            {
                Console.WriteLine($"Produkt med ID {productId} finns ej.");
            }
        }


        public void UpdateProductName(string productId, string newName)
        {
            if (availableProducts.ContainsKey(productId))
            {
                availableProducts[productId].Name = newName;
                SaveProductsToFile();
                Console.WriteLine($"Produktnamn uppdaterat på: {productId}.");
            }
            else
            {
                Console.WriteLine($"Produkt med ID {productId} finns ej.");
            }
        }

        public void UpdateProductPrice(string productId, decimal newUnitPrice, decimal newKiloPrice)
        {
            if (availableProducts.ContainsKey(productId))
            {
                availableProducts[productId].UnitPrice = newUnitPrice;
                availableProducts[productId].KiloPrice = newKiloPrice;
                SaveProductsToFile(); 
                Console.WriteLine($"Produktpris uppdaterat med ID {productId}.");
            }
            else
            {
                Console.WriteLine($"Produkt med ID {productId} finns ej.");
            }
        }

        public void RemoveProduct(string productId)
        {
            if (availableProducts.ContainsKey(productId))
            {
                availableProducts.Remove(productId);
                SaveProductsToFile();
                Console.WriteLine($"Produkt med ID {productId} har tagits bort.");
            }
            else
            {
                Console.WriteLine($"Produkt med ID {productId} finns ej.");
            }
        }

        /// <summary>
        /// Saves all products to Produkter.txt file
        /// </summary>
        public void SaveProductsToFile()
        {
            try
            {
                Directory.CreateDirectory($"../../../Produkter");
                using (StreamWriter writer = new StreamWriter("../../../Produkter/Produkter.txt"))
                {
                    foreach (var product in availableProducts.Values)
                    {
                        string discountInfo = $"{product.Discount:F2} {product.DiscountStartDate:yyyy-MM-dd} {product.DiscountEndDate:yyyy-MM-dd}";
                        writer.WriteLine($"{product.Id} {product.Name} {product.UnitPrice} {product.KiloPrice} {discountInfo}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparandet av produktlistan: {ex.Message}");
            }
        }

        public void LoadProductsFromFile()
        {
            if (File.Exists("../../../Produkter/Produkter.txt"))
            {
                using (StreamReader reader = new StreamReader("../../../Produkter/Produkter.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(' ');
                        if (parts.Length >= 4)
                        {
                            string id = parts[0].Trim();
                            string name = parts[1].Trim();
                            decimal unitPrice = decimal.Parse(parts[2].Trim());
                            decimal kiloPrice = decimal.Parse(parts[3].Trim());

                            decimal discount = 0; 
                            DateTime discountStartDate = DateTime.MinValue;
                            DateTime discountEndDate = DateTime.MinValue;

                            if (parts.Length >= 7)  
                            {
                                discount = decimal.Parse(parts[4].Trim());
                                discountStartDate = DateTime.Parse(parts[5].Trim());
                                discountEndDate = DateTime.Parse(parts[6].Trim());
                            }

                            availableProducts[id] = new Product(id, name, unitPrice, kiloPrice)
                            {
                                Discount = discount,
                                DiscountStartDate = discountStartDate,
                                DiscountEndDate = discountEndDate
                            };

                        }
                    }
                }
            }
        }

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

        public void AddProduct(string productId, string productName, decimal unitPrice, decimal kiloPrice)
        {
            if (!availableProducts.ContainsKey(productId))
            {
                if (unitPrice == kiloPrice || kiloPrice == 0)
                {
                    if (unitPrice <= 0 || kiloPrice < 0)
                    {
                        Console.WriteLine("\nDu kan inte ange 0 i styckpris!");
                        return;
                    }
                    availableProducts[productId] = new Product(productId, productName, unitPrice, kiloPrice);
                    SaveProductsToFile();
                    Console.WriteLine($"Produkt med ID {productId} har lagts till.");
                }
                else
                {
                    Console.WriteLine("Styckpris och kilopris måste vara lika.");
                }
            }
            else
            {
                Console.WriteLine($"Produkt med ID {productId} finns redan.");
            }
        }

        public void DisplayAvailableProducts()
        {
            Console.WriteLine("╭─────────────────────────────╮");
            Console.WriteLine("│    Tillgängliga produkter   │");
            Console.WriteLine("╰─────────────────────────────╯");
            Console.WriteLine("ID  Produkt                Pris");

            foreach (var product in availableProducts.Values)
            {
                string priceInfo = product.IsKiloPrice ? $"{product.KiloPrice:F2} kr/kg" : $"{product.UnitPrice:F2} kr/st";

                string productInfo = product.Discount > 0 ? $"{product.Name} ({product.Discount}%)*" : product.Name;

                Console.WriteLine($"{product.Id.PadRight(4)}{productInfo.PadRight(22)}{priceInfo}");
            }

        }
    }
}
