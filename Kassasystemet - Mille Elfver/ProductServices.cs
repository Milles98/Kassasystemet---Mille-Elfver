using Kassasystemet___Mille_Elfver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ProductServices : IProductServices
    {
        private IProductCatalog _productCatalog;
        private IFileManager _fileManager;

        public ProductServices()
        {
            _productCatalog = ProductCatalog.Instance;
            _fileManager = new FileManager();
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
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            if (products.ContainsKey(productId))
            {
                Product product = products[productId];

                product.Discounts.PercentageDiscount = percentageDiscount;
                product.Discounts.PercentStartDate = startDate;
                product.Discounts.PercentEndDate = endDate;

                _fileManager.SaveProductsToFile(products);

                InputSuccessMessage($"Kampanjpris satt på produkt med ID {productId}.");

            }
            else
            {
                ErrorMessage($"\nProdukt med ID {productId} finns ej.");
            }
        }
        public void SetQuantityDiscount(string productId, int buyQuantity, int payForQuantity, DateTime startDate, DateTime endDate)
        {
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            if (products.ContainsKey(productId))
            {
                Product product = products[productId];

                if (!product.IsKiloPrice && buyQuantity > payForQuantity)
                {
                    product.Discounts.BuyQuantity = buyQuantity;
                    product.Discounts.PayForQuantity = payForQuantity;
                    product.Discounts.BuyQuantityStartDate = startDate;
                    product.Discounts.BuyQuantityEndDate = endDate;

                    _fileManager.SaveProductsToFile(products);

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
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            if (products.ContainsKey(productId) && (products[productId].Discounts.PercentageDiscount > 0
                || products[productId].Discounts.BuyQuantity > 0))
            {
                products[productId].Discounts = new ProductDiscount(0, 0, 0, DateTime.MinValue,
                    DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);

                _fileManager.SaveProductsToFile(products);

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
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            if (products.ContainsKey(productId))
            {
                products[productId].Name = newName;
                _fileManager.SaveProductsToFile(products);

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
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            if (products.ContainsKey(productId))
            {
                if (newPrice <= 0)
                {
                    ErrorMessage("Ogiltigt pris. Priset måste vara större än noll.");
                    return;
                }
                if (priceType.ToLower() == "s")
                {
                    products[productId].UnitPrice = newPrice;
                    products[productId].KiloPrice = 0;
                }
                else if (priceType.ToLower() == "k")
                {
                    products[productId].KiloPrice = newPrice;
                    products[productId].UnitPrice = 0;
                }
                else
                {
                    ErrorMessage("Ogiltig inmatning för pristyp. Ange 'S' för styckpris eller 'K' för kilopris.");
                    return;
                }

                _fileManager.SaveProductsToFile(products);
                _fileManager.LoadProductsFromFile(products);

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
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            if (products.ContainsKey(productId))
            {
                products.Remove(productId);
                _fileManager.SaveProductsToFile(products);

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
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            return products.ContainsKey(productId);
        }

        /// <summary>
        /// Checks if productId input exists (example 300-320) otherwise returns null
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProduct(string productId)
        {
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            if (products.TryGetValue(productId, out var product))
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
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            if (!products.ContainsKey(productId))
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
                products[productId] = new Product(productId, productName, unitPrice, kiloPrice);
                _fileManager.SaveProductsToFile(products);
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
            Dictionary<string, Product> products = _productCatalog.GetProducts();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╭─────────────────────────────╮");
            Console.WriteLine("│    Tillgängliga produkter   │");
            Console.WriteLine("╰─────────────────────────────╯");
            Console.WriteLine("ID  Produkt                Pris          Rabatter");
            Console.ResetColor();

            foreach (var product in products.Values)
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
