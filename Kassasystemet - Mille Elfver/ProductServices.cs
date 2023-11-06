using Kassasystemet___Mille_Elfver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ProductServices
    {
        private Dictionary<string, Product> _availableProducts = new Dictionary<string, Product>();

        public ProductServices()
        {
            DataSeeding();
        }

        /// <summary>
        /// Loads all products and saves them to Produkter.txt
        /// </summary>
        public void DataSeeding()
        {
            LoadProductsFromFile();

            StartingItems();

            SaveProductsToFile();
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
            if (_availableProducts.ContainsKey(id))
            {
                _availableProducts[id].Name = name;
                _availableProducts[id].UnitPrice = unitPrice;
                _availableProducts[id].KiloPrice = kiloPrice;
            }
            else
            {
                var product = new Product(id, name, unitPrice, kiloPrice);
                _availableProducts.Add(product.Id, product);
            }
        }
        /// <summary>
        /// Creates percentage discount on products
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="percentageDiscount"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void SetPercentageDiscount(string productId, decimal percentageDiscount, DateTime startDate, DateTime endDate)
        {
            if (_availableProducts.ContainsKey(productId))
            {
                Product product = _availableProducts[productId];

                product.Discounts.PercentageDiscount = percentageDiscount;
                product.Discounts.PercentStartDate = startDate;
                product.Discounts.PercentEndDate = endDate;

                SaveProductsToFile();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Kampanjpris satt på produkt med ID {productId}.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
                Console.ResetColor();
            }
        }
        public void SetQuantityDiscount(string productId, int buyQuantity, int payForQuantity, DateTime startDate, DateTime endDate)
        {
            if (_availableProducts.ContainsKey(productId))
            {
                Product product = _availableProducts[productId];

                if (!product.IsKiloPrice && buyQuantity > payForQuantity)
                {
                    product.Discounts.BuyQuantity = buyQuantity;
                    product.Discounts.PayForQuantity = payForQuantity;
                    product.Discounts.BuyQuantityStartDate = startDate;
                    product.Discounts.BuyQuantityEndDate = endDate;

                    SaveProductsToFile();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Mängdrabatt satt på produkt med ID {productId}.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nProdukt med ID {productId} har kilopris och kan inte ha mängdrabatt.");
                    Console.WriteLine($"OBS du kan inte välja exempelvis köp 5 betala för 10!");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Removes the current discounts on product
        /// </summary>
        /// <param name="productId"></param>
        public void RemoveDiscount(string productId)
        {
            if (_availableProducts.ContainsKey(productId) && (_availableProducts[productId].Discounts.PercentageDiscount > 0
                || _availableProducts[productId].Discounts.BuyQuantity > 0))
            {
                _availableProducts[productId].Discounts = new ProductDiscount(0, 0, 0, DateTime.MinValue,
                    DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

                SaveProductsToFile();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Rabatt har tagits bort för produkt med ID {productId}.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Updates the products name
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newName"></param>
        public void UpdateProductName(string productId, string newName)
        {
            if (_availableProducts.ContainsKey(productId))
            {
                _availableProducts[productId].Name = newName;
                SaveProductsToFile();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Produktnamn uppdaterat på: {productId}.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
                Console.ResetColor();
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
            if (_availableProducts.ContainsKey(productId))
            {
                if (newPrice <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltigt pris. Priset måste vara större än noll.");
                    Console.ResetColor();
                    return;
                }
                if (priceType.ToLower() == "s")
                {
                    _availableProducts[productId].UnitPrice = newPrice;
                    _availableProducts[productId].KiloPrice = 0;
                }
                else if (priceType.ToLower() == "k")
                {
                    _availableProducts[productId].KiloPrice = newPrice;
                    _availableProducts[productId].UnitPrice = 0;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltig inmatning för pristyp. Ange 'S' för styckpris eller 'K' för kilopris.");
                    Console.ResetColor();
                    return;
                }

                SaveProductsToFile();
                LoadProductsFromFile();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Produktpris uppdaterat med ID {productId}.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
                Console.ResetColor();
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
                Console.ResetColor();
                return;
            }

            AddProduct(productId, productName, unitPrice, kiloPrice, priceType);
        }

        /// <summary>
        /// Removes a product from the dictionary
        /// </summary>
        /// <param name="productId"></param>
        public void RemoveProduct(string productId)
        {
            if (_availableProducts.ContainsKey(productId))
            {
                _availableProducts.Remove(productId);
                SaveProductsToFile();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Produkt med ID {productId} har tagits bort.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nProdukt med ID {productId} finns ej.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Saves all products to Produkter map & Produkter.txt file, if it doesnt exist, creates one
        /// </summary>
        public void SaveProductsToFile()
        {
            try
            {
                Directory.CreateDirectory($"../../../Produkter");
                using (StreamWriter writer = new StreamWriter("../../../Produkter/Produkter.txt"))
                {
                    foreach (var product in _availableProducts.Values)
                    {
                        string discountInfo = $"{product.Discounts.PercentageDiscount:F2}";

                        string dateInfo = $"{product.Discounts.PercentStartDate:yyyy-MM-dd}|{product.Discounts.PercentEndDate:yyyy-MM-dd}";

                        string amountDiscountInfo = $"{product.Discounts.BuyQuantity}|{product.Discounts.PayForQuantity}|" +
                            $"{product.Discounts.BuyQuantityStartDate:yyyy-MM-dd}|{product.Discounts.BuyQuantityEndDate:yyyy-MM-dd}";

                        writer.WriteLine($"{product.Id}|{product.Name}|{product.UnitPrice}|{product.KiloPrice}|" +
                            $"{discountInfo}|{dateInfo}|{amountDiscountInfo}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel vid sparandet av produktlistan: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Checks if produkter map & txt file exists, then loads all products onto it
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
                            int buyQuantity = 0;
                            int payForQuantity = 0;
                            DateTime discountStartDate = DateTime.MinValue;
                            DateTime discountEndDate = DateTime.MinValue;
                            DateTime buyQuantityStartDate = DateTime.MinValue;
                            DateTime buyQuantityEndDate = DateTime.MinValue;

                            if (parts.Length >= 7)
                            {
                                discount = decimal.Parse(parts[4].Trim());
                                discountStartDate = DateTime.Parse(parts[5].Trim());
                                discountEndDate = DateTime.Parse(parts[6].Trim());
                            }

                            if (parts.Length >= 9)
                            {
                                buyQuantity = int.Parse(parts[7].Trim());
                                payForQuantity = int.Parse(parts[8].Trim());
                            }

                            if (parts.Length >= 11)
                            {
                                buyQuantity = int.Parse(parts[7].Trim());
                                payForQuantity = int.Parse(parts[8].Trim());

                                buyQuantityStartDate = DateTime.Parse(parts[9].Trim());
                                buyQuantityEndDate = DateTime.Parse(parts[10].Trim());
                            }

                            _availableProducts[id] = new Product(id, name, unitPrice, kiloPrice)
                            {
                                Discounts = new ProductDiscount(discount, buyQuantity, payForQuantity, discountStartDate,
                                discountEndDate, buyQuantityStartDate, buyQuantityEndDate)
                            };

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the product ID exists
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool ProductExists(string productId)
        {
            return _availableProducts.ContainsKey(productId);
        }

        /// <summary>
        /// Checks if productId input exists (example 300-320) otherwise returns null
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProduct(string productId)
        {
            if (_availableProducts.TryGetValue(productId, out var product))
            {
                return product;
            }
            return null;
        }

        /// <summary>
        /// Adds new product onto the dictionary
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productName"></param>
        /// <param name="unitPrice"></param>
        /// <param name="kiloPrice"></param>
        public void AddProduct(string productId, string productName, decimal unitPrice, decimal kiloPrice, string priceType)
        {
            if (!_availableProducts.ContainsKey(productId))
            {
                if (priceType.ToLower() == "s" && unitPrice <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nOgiltigt styckpris. Styckpriset måste vara större än 0.");
                    Console.ResetColor();
                    return;
                }
                if (priceType.ToLower() == "k" && kiloPrice <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nOgiltigt kilopris. Kilopriset måste vara 0 eller större.");
                    Console.ResetColor();
                    return;
                }
                _availableProducts[productId] = new Product(productId, productName, unitPrice, kiloPrice);
                SaveProductsToFile();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Produkt med ID {productId} har lagts till.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nProdukt med ID {productId} finns redan.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Displays all currently available products including discounts
        /// </summary>
        public void DisplayAvailableProducts()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╭─────────────────────────────╮");
            Console.WriteLine("│    Tillgängliga produkter   │");
            Console.WriteLine("╰─────────────────────────────╯");
            Console.WriteLine("ID  Produkt                Pris         Rabatter");
            Console.ResetColor();

            foreach (var product in _availableProducts.Values)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string priceInfo = product.IsKiloPrice ? $"{product.KiloPrice:F2} kr/kg" : $"{product.UnitPrice:F2} kr/st"; //ternary!
                string productInfo = product.Name;
                string discountInfo = string.Empty;

                if (product.Discounts.PercentageDiscount > 0)
                {
                    if (DateTime.Today >= product.Discounts.PercentStartDate && DateTime.Today <= product.Discounts.PercentEndDate)
                    {
                        discountInfo = $" PÅGÅENDE: ({product.Discounts.PercentageDiscount}%) t.o.m. {product.Discounts.PercentEndDate:yyyy-MM-dd}";
                    }
                    else if (product.Discounts.PercentStartDate > DateTime.Today)
                    {
                        discountInfo = $"*KOMMANDE: ({product.Discounts.PercentageDiscount}%) fr.o.m. {product.Discounts.PercentStartDate:yyyy-MM-dd}";
                    }
                    else
                    {
                        discountInfo = "(Rabatt har utgått)";
                        product.Discounts.PercentageDiscount = 0;
                        product.Discounts.PercentStartDate = DateTime.MinValue;
                        product.Discounts.PercentEndDate = DateTime.MinValue;
                    }
                }

                if (product.Discounts.BuyQuantity > 0 && product.Discounts.PayForQuantity > 0)
                {
                    if (!string.IsNullOrEmpty(discountInfo))
                    {
                        discountInfo += ", ";
                    }

                    if (DateTime.Today >= product.Discounts.BuyQuantityStartDate && DateTime.Today <= product.Discounts.BuyQuantityEndDate)
                    {
                        discountInfo += $" PÅGÅENDE: (Köp {product.Discounts.BuyQuantity}, Betala för {product.Discounts.PayForQuantity}) " +
                            $"t.o.m. {product.Discounts.BuyQuantityEndDate:yyyy-MM-dd}";
                    }
                    else if (product.Discounts.BuyQuantityStartDate > DateTime.Today)
                    {
                        discountInfo = $"*KOMMANDE: (Köp {product.Discounts.BuyQuantity}, Betala för {product.Discounts.PayForQuantity}) " +
                            $"fr.o.m. {product.Discounts.BuyQuantityStartDate:yyyy-MM-dd}";
                    }
                    else
                    {
                        discountInfo += "(Rabatt har utgått)";
                        product.Discounts.BuyQuantity = 0;
                        product.Discounts.PayForQuantity = 0;
                        product.Discounts.BuyQuantityStartDate = DateTime.MinValue;
                        product.Discounts.BuyQuantityEndDate = DateTime.MinValue;
                    }
                }

                Console.WriteLine($"{product.Id.PadRight(4)}{productInfo.PadRight(22)}{priceInfo.PadRight(14)}{discountInfo.PadRight(40)}");
                Console.ResetColor();
            }

        }
    }
}
