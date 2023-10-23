using Kassasystemet___Mille_Elfver;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Contains all the pre-defined products for the cashregister
        /// </summary>
        private void StartingItems()
        {
            AddProductToDictionary("300", "Bananer", 0, 15.50m);
            AddProductToDictionary("301", "Tomater", 0, 79.90m);
            AddProductToDictionary("302", "Apelsiner", 0, 9.90m);
            AddProductToDictionary("303", "Godis", 0, 99.50m);
            AddProductToDictionary("304", "Kött", 0, 199m);
            AddProductToDictionary("305", "Kyckling", 0, 129.90m);
            AddProductToDictionary("306", "Nutella", 21.90m, 0);
            AddProductToDictionary("307", "Citron", 5.50m, 0);
            AddProductToDictionary("308", "Jordgubbar", 39.90m, 0);
            AddProductToDictionary("309", "Grädde", 24.90m, 0);
            AddProductToDictionary("310", "Choklad", 22.90m, 0);
            AddProductToDictionary("311", "Mango", 19.90m, 0);
            AddProductToDictionary("312", "Öl", 15.50m, 0);
            AddProductToDictionary("313", "Kex", 23.90m, 0);
            AddProductToDictionary("314", "Gurka", 10m, 0);
            AddProductToDictionary("315", "Leverpastej", 24.90m, 0);
            AddProductToDictionary("316", "Chips", 27.90m, 0);
            AddProductToDictionary("317", "Bröd", 34.90m, 0);
            AddProductToDictionary("318", "Mjölk", 16.90m, 0);
            AddProductToDictionary("319", "Yoghurt", 29.90m, 0);
            AddProductToDictionary("320", "Glass", 39.90m, 0);
        }

        /// <summary>
        /// Adds products to the dictionary
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="unitPrice"></param>
        /// <param name="kiloPrice"></param>
        private void AddProductToDictionary(string id, string name, decimal unitPrice, decimal kiloPrice)
        {
            var product = new Product(id, name, unitPrice, kiloPrice);
            availableProducts.Add(product.Id, product);
        }
        /// <summary>
        /// Creates discount on products
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="discount"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void SetDiscount(string productId, decimal discount, DateTime startDate, DateTime endDate)
        {
            if (availableProducts.ContainsKey(productId))
            {
                Product product = availableProducts[productId];
                product.Discount = discount;
                product.DiscountStartDate = startDate;
                product.DiscountEndDate = endDate;
                SaveProductsToFile();
                Console.WriteLine($"\nKampanjpris satt på produkt med ID {productId}.");
            }
            else
            {
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
            }
        }

        /// <summary>
        /// Removes discount on products
        /// </summary>
        /// <param name="productId"></param>
        public void RemoveDiscount(string productId)
        {
            if (availableProducts.ContainsKey(productId) && availableProducts[productId].Discount > 0)
            {
                availableProducts[productId].Discount = 0;
                availableProducts[productId].DiscountStartDate = DateTime.MinValue;
                availableProducts[productId].DiscountEndDate = DateTime.MinValue;
                SaveProductsToFile();
                Console.WriteLine($"\nRabatt har tagits bort för produkt med ID {productId}.");
            }
            else
            {
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
            }
        }

        /// <summary>
        /// Updates the products name
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newName"></param>
        public void UpdateProductName(string productId, string newName)
        {
            if (availableProducts.ContainsKey(productId))
            {
                availableProducts[productId].Name = newName;
                SaveProductsToFile();
                Console.WriteLine($"\nProduktnamn uppdaterat på: {productId}.");
            }
            else
            {
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
            }
        }

        /// <summary>
        /// Updates the products price
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newUnitPrice"></param>
        /// <param name="newKiloPrice"></param>
        public void UpdateProductPrice(string productId, decimal newPrice, string priceType)
        {
            if (availableProducts.ContainsKey(productId))
            {
                if (newPrice <= 0)
                {
                    Console.WriteLine("Ogiltigt pris. Priset måste vara större än noll.");
                    return;
                }
                if (priceType.ToLower() == "s")
                {
                    availableProducts[productId].UnitPrice = newPrice;
                    availableProducts[productId].KiloPrice = 0;
                }
                else if (priceType.ToLower() == "k")
                {
                    availableProducts[productId].KiloPrice = newPrice;
                    availableProducts[productId].UnitPrice = 0;
                }
                else
                {
                    Console.WriteLine("Ogiltig inmatning för pristyp. Ange 'S' för styckpris eller 'K' för kilopris.");
                    return;
                }

                SaveProductsToFile();
                LoadProductsFromFile();
                Console.WriteLine($"\nProduktpris uppdaterat med ID {productId}.");
            }
            else
            {
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
            }
        }

        public void AddProductWithPriceType(string productId, string productName, string priceType, decimal price)
        {
            decimal unitPrice = 0;
            decimal kiloPrice = 0;

            if (priceType == "s")
            {
                unitPrice = price;
            }
            else if (priceType == "k")
            {
                kiloPrice = price;
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
                return;
            }

            AddProduct(productId, productName, unitPrice, kiloPrice, priceType);
        }

        /// <summary>
        /// Removes a product
        /// </summary>
        /// <param name="productId"></param>
        public void RemoveProduct(string productId)
        {
            if (availableProducts.ContainsKey(productId))
            {
                availableProducts.Remove(productId);
                SaveProductsToFile();
                Console.WriteLine($"\nProdukt med ID {productId} har tagits bort.");
            }
            else
            {
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
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
                        string discountInfo = $"{product.Discount:F2}|{product.DiscountStartDate:yyyy-MM-dd}|{product.DiscountEndDate:yyyy-MM-dd}";
                        writer.WriteLine($"{product.Id}|{product.Name}|{product.UnitPrice}|{product.KiloPrice}|{discountInfo}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparandet av produktlistan: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if file exists, then loads all products onto it
        /// </summary>
        public void LoadProductsFromFile()
        {
            if (File.Exists("../../../Produkter/Produkter.txt"))
            {
                using (StreamReader reader = new StreamReader("../../../Produkter/Produkter.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split('|');
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

        public bool ProductExists(string productId)
        {
            return availableProducts.ContainsKey(productId);
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

        /// <summary>
        /// Adds product onto the dictionary
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productName"></param>
        /// <param name="unitPrice"></param>
        /// <param name="kiloPrice"></param>
        public void AddProduct(string productId, string productName, decimal unitPrice, decimal kiloPrice, string priceType)
        {
            if (!availableProducts.ContainsKey(productId))
            {
                if (priceType.ToLower() == "s" && unitPrice <= 0)
                {
                    Console.WriteLine("\nOgiltigt styckpris. Styckpriset måste vara större än 0.");
                    return;
                }
                if (priceType.ToLower() == "k" && kiloPrice < 0)
                {
                    Console.WriteLine("\nOgiltigt kilopris. Kilopriset måste vara 0 eller större.");
                    return;
                }
                availableProducts[productId] = new Product(productId, productName, unitPrice, kiloPrice);
                SaveProductsToFile();
                Console.WriteLine($"\nProdukt med ID {productId} har lagts till.");
            }
            else
            {
                Console.WriteLine($"\nProdukt med ID {productId} finns redan.");
            }
        }

        /// <summary>
        /// Displays all currently available products
        /// </summary>
        public void DisplayAvailableProducts()
        {
            Console.WriteLine("╭─────────────────────────────╮");
            Console.WriteLine("│    Tillgängliga produkter   │");
            Console.WriteLine("╰─────────────────────────────╯");
            Console.WriteLine("ID  Produkt                Pris         Rabatter");

            foreach (var product in availableProducts.Values)
            {
                string priceInfo = product.IsKiloPrice ? $"{product.KiloPrice:F2} kr/kg" : $"{product.UnitPrice:F2} kr/st";

                string productInfo = product.Name;
                string discountInfo = string.Empty;

                if (product.Discount > 0)
                {
                    discountInfo = $"({product.Discount}%)";

                    if (DateTime.Today < product.DiscountStartDate)
                    {
                        discountInfo += $" (börjar {product.DiscountStartDate:yyyy-MM-dd})";
                    }
                }

                Console.WriteLine($"{product.Id.PadRight(4)}{productInfo.PadRight(22)}{priceInfo.PadRight(14)}{discountInfo.PadRight(16)}");
            }

        }
    }
}
