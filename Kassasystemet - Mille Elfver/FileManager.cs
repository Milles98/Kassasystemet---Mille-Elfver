using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kassasystemet___Mille_Elfver.Interfaces;

namespace Kassasystemet___Mille_Elfver
{
    public class FileManager : IFileManager
    {

        /// <summary>
        /// Saves all products to Produkter map & Produkter.txt file, if it doesnt exist, creates one
        /// </summary>
        public void SaveProductsToFile(Dictionary<string, Product> availableProducts)
        {
            try
            {
                Directory.CreateDirectory("../../../Produkter");
                using (StreamWriter writer = new StreamWriter("../../../Produkter/Produkter.txt"))
                {
                    foreach (var product in availableProducts.Values)
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
                ErrorMessage($"Fel vid sparandet av produktlistan: {ex.Message}");
            }
        }

        /// <summary>
        /// Checks if produkter map & txt file exists, then loads all products onto it
        /// </summary>
        public void LoadProductsFromFile(Dictionary<string, Product> availableProducts)
        {
            if (File.Exists("../../../Produkter/Produkter.txt"))
            {
                availableProducts.Clear();
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

                            if (parts.Length >= 11)
                            {
                                buyQuantity = int.Parse(parts[7].Trim());
                                payForQuantity = int.Parse(parts[8].Trim());

                                buyQuantityStartDate = DateTime.Parse(parts[9].Trim());
                                buyQuantityEndDate = DateTime.Parse(parts[10].Trim());
                            }

                            availableProducts[id] = new Product(id, name, unitPrice, kiloPrice)
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
        /// Saves the last receipt number to "KvittoRäknare" textfile and creates if it doesnt exist
        /// </summary>
        public void SaveReceiptCounter(int receiptCounter)
        {
            try
            {
                Directory.CreateDirectory("../../../Kvitton");
                File.WriteAllText("../../../Kvitton/KvittoRäknare.txt", receiptCounter.ToString());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel vid sparandet av kvittoräknare: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Loads the latest receipt number
        /// </summary>
        public int LoadReceiptCounter()
        {
            try
            {
                if (File.Exists("../../../Kvitton/KvittoRäknare.txt"))
                {
                    string counterText = File.ReadAllText("../../../Kvitton/KvittoRäknare.txt");
                    if (int.TryParse(counterText, out int counter))
                    {
                        return counter;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel vid laddandet av kvittoräknare: {ex.Message}");
                Console.ResetColor();
            }

            return 0;
        }

        /// <summary>
        /// Takes variable receiptText from GenerateReceipt method and saves it to Kvitto.txt file
        /// </summary>
        /// <param name="receiptText"></param>
        public void SaveReceipt(string receiptText)
        {
            try
            {
                var date = DateTime.Now.ToShortDateString();
                string fileName = $"Kvitton - {date}.txt";
                Directory.CreateDirectory($"../../../Kvitton");
                File.AppendAllText($"../../../Kvitton/{fileName}", receiptText);
                Console.WriteLine(receiptText);
            }
            catch (Exception ex)
            {
                ErrorMessage($"Fel vid sparandet av kvitto: {ex.Message}");
            }
        }
        /// <summary>
        /// Error message
        /// </summary>
        /// <param name="message"></param>
        private void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
