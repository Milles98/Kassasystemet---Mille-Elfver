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
        private Dictionary<string, Product> _products = new Dictionary<string, Product>();
        private FileManager _fileManager = new FileManager();

        public ProductServices()
        {
            DataSeeding();
        }

        /// <summary>
        /// Loads all products and saves them to Produkter.txt
        /// </summary>
        public void DataSeeding()
        {
            _fileManager.LoadProductsFromFile(_products);

            if (_products.Count == 0)
            {
                StartingItems();
                _fileManager.SaveProductsToFile(_products);
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
        /// <param name="productId"></param>
        /// <param name="name"></param>
        /// <param name="unitPrice"></param>
        /// <param name="kiloPrice"></param>
        private void AddProductToDictionary(string productId, string name, decimal unitPrice, decimal kiloPrice)
        {
            if (_products.ContainsKey(productId))
            {
                _products[productId].Name = name;
                _products[productId].UnitPrice = unitPrice;
                _products[productId].KiloPrice = kiloPrice;
            }
            else
            {
                var product = new Product(productId, name, unitPrice, kiloPrice);
                _products.Add(product.Id, product);
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
            if (_products.ContainsKey(productId))
            {
                Product product = _products[productId];

                product.Discounts.PercentageDiscount = percentageDiscount;
                product.Discounts.PercentStartDate = startDate;
                product.Discounts.PercentEndDate = endDate;

                _fileManager.SaveProductsToFile(_products);

                InputSuccessMessage($"Kampanjpris satt på produkt med ID {productId}.");

            }
            else
            {
                ErrorMessage($"\nProdukt med ID {productId} finns ej.");
            }
        }
        public void SetQuantityDiscount(string productId, int buyQuantity, int payForQuantity, DateTime startDate, DateTime endDate)
        {
            if (_products.ContainsKey(productId))
            {
                Product product = _products[productId];

                if (!product.IsKiloPrice && buyQuantity > payForQuantity)
                {
                    product.Discounts.BuyQuantity = buyQuantity;
                    product.Discounts.PayForQuantity = payForQuantity;
                    product.Discounts.BuyQuantityStartDate = startDate;
                    product.Discounts.BuyQuantityEndDate = endDate;

                    _fileManager.SaveProductsToFile(_products);

                    InputSuccessMessage($"Mängdrabatt satt på produkt med ID {productId}.");
                }
                else
                {
                    ErrorMessage($"\nProdukt med ID {productId} har kilopris och kan inte ha mängdrabatt." +
                        $"\nOBS du kan inte välja exempelvis köp 5 betala för 10!");
                }
            }
            else
            {
                ErrorMessage($"\nProdukt med ID {productId} finns ej.");
            }
        }

        /// <summary>
        /// Removes the current discounts on product
        /// </summary>
        /// <param name="productId"></param>
        public void RemoveDiscount(string productId)
        {
            if (_products.ContainsKey(productId) && (_products[productId].Discounts.PercentageDiscount > 0
                || _products[productId].Discounts.BuyQuantity > 0))
            {
                _products[productId].Discounts = new ProductDiscount(0, 0, 0, DateTime.MinValue,
                    DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

                _fileManager.SaveProductsToFile(_products);

                InputSuccessMessage($"Rabatt har tagits bort för produkt med ID {productId}.");
            }
            else
            {
                ErrorMessage($"\nProdukt med ID {productId} finns ej.");
            }
        }

        /// <summary>
        /// Updates the products name
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="newName"></param>
        public void UpdateProductName(string productId, string newName)
        {
            if (_products.ContainsKey(productId))
            {
                _products[productId].Name = newName;
                _fileManager.SaveProductsToFile(_products);

                InputSuccessMessage($"Produktnamn uppdaterat på: {productId}.");
            }
            else
            {
                ErrorMessage($"\nProdukt med ID {productId} finns ej.");
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
            if (_products.ContainsKey(productId))
            {
                if (newPrice <= 0)
                {
                    ErrorMessage("Ogiltigt pris. Priset måste vara större än noll.");
                    return;
                }
                if (priceType.ToLower() == "s")
                {
                    _products[productId].UnitPrice = newPrice;
                    _products[productId].KiloPrice = 0;
                }
                else if (priceType.ToLower() == "k")
                {
                    _products[productId].KiloPrice = newPrice;
                    _products[productId].UnitPrice = 0;
                }
                else
                {
                    ErrorMessage("Ogiltig inmatning för pristyp. Ange 'S' för styckpris eller 'K' för kilopris.");
                    return;
                }

                _fileManager.SaveProductsToFile(_products);
                _fileManager.LoadProductsFromFile(_products);

                InputSuccessMessage($"Produktpris uppdaterat med ID {productId}.");
            }
            else
            {
                ErrorMessage($"\nProdukt med ID {productId} finns ej.");
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
                ErrorMessage("Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
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
            if (_products.ContainsKey(productId))
            {
                _products.Remove(productId);
                _fileManager.SaveProductsToFile(_products);

                InputSuccessMessage($"Produkt med ID {productId} har tagits bort.");
            }
            else
            {
                ErrorMessage($"\nProdukt med ID {productId} finns ej.");
            }
        }

        /// <summary>
        /// Checks if the product ID exists
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool ProductExists(string productId)
        {
            return _products.ContainsKey(productId);
        }

        /// <summary>
        /// Checks if productId input exists (example 300-320) otherwise returns null
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProduct(string productId)
        {
            if (_products.TryGetValue(productId, out var product))
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
            if (!_products.ContainsKey(productId))
            {
                if (priceType.ToLower() == "s" && unitPrice <= 1)
                {
                    ErrorMessage("\nOgiltigt styckpris. Styckpriset måste vara större än 0.");
                    return;
                }
                if (priceType.ToLower() == "k" && kiloPrice <= 1)
                {
                    ErrorMessage("\nOgiltigt kilopris. Kilopriset måste vara större än 0.");
                    return;
                }
                _products[productId] = new Product(productId, productName, unitPrice, kiloPrice);
                _fileManager.SaveProductsToFile(_products);
                InputSuccessMessage($"Produkt med ID {productId} har lagts till.");
            }
            else
            {
                ErrorMessage($"\nProdukt med ID {productId} finns redan.");
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
            Console.WriteLine("ID  Produkt                Pris          Rabatter");
            Console.ResetColor();

            foreach (var product in _products.Values)
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
        private void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private void InputSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
