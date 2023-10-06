using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ShoppingCart
    {
        private StringBuilder receipt = new StringBuilder();

        /// <summary>
        /// Adds the productID and amount to receipt StringBuilder
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddingToReceipt(Product product, decimal quantity)
        {
            Console.Clear();
            if (product != null) //kollar så produkten inte är null
            {
                decimal totalPrice;
                string productInfo;

                if (product.IsKiloPrice)
                {
                    totalPrice = product.KiloPrice * quantity;
                    productInfo = $"{product.Name.PadRight(15)} {quantity:F2} kg * {product.KiloPrice:F2}";
                }
                else
                {
                    totalPrice = product.UnitPrice * quantity;
                    productInfo = $"{product.Name.PadRight(15)} {quantity} st * {product.UnitPrice:F2}";
                }

                int paddingSpaces = Math.Max(0, 40 - productInfo.Length - totalPrice.ToString("F2").Length);

                string productToAdd = $"{productInfo}{new string(' ', paddingSpaces)}{totalPrice:F2}";

                receipt.AppendLine(productToAdd);

                string priceType = product.IsKiloPrice ? "kilo" : "st"; //ternary

                Console.WriteLine($"Produkt: {product.Name}, {quantity} {priceType:F2}, totalt pris: {totalPrice:F2} kr har lagts till på kvittot.");
            }
            else
            {
                Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                ProductCatalog productCatalog = new ProductCatalog();
                productCatalog.DisplayAvailableProducts();
            }
        }

        /// <summary>
        /// Calculates all the products added to receipt StringBuilder
        /// </summary>
        /// <returns></returns>
        public decimal CalculateTotal()
        {
            string[] linesInReceipt = receipt.ToString().Split('\n');

            decimal totalAmount = 0m;

            foreach (string line in linesInReceipt)
            {
                string[] partsInReceipt = line.Split(' ');

                if (partsInReceipt.Length >= 4)
                {
                    if (decimal.TryParse(partsInReceipt[partsInReceipt.Length - 1], out decimal totalPrice))
                    {
                        totalAmount += totalPrice;
                    }
                }
            }

            return totalAmount;
        }

        /// <summary>
        /// Gets variable totalAmount from CalculateTotal method and generates a functional receipt
        /// </summary>
        /// <returns></returns>
        public string GenerateReceipt()
        {
            decimal totalAmount = CalculateTotal();

            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            string receiptSeparator = new string('-', 40);
            int maxWidth = 40;
            int availableSpace = maxWidth - "Total:".Length;
            string totalAmountText = totalAmount.ToString("C");
            string totalAmountPadding = new string(' ', availableSpace - totalAmountText.Length);
            string totalLine = $"\nTotal:{totalAmountPadding}{totalAmountText}";

            StringBuilder receiptText = new StringBuilder();
            receiptText.AppendLine(receiptSeparator);
            receiptText.AppendLine($"KVITTO {formattedDate.PadLeft(33)}\n");

            receiptText.Append(receipt.ToString());

            receiptText.AppendLine(totalLine);
            receiptText.AppendLine(receiptSeparator);

            return receiptText.ToString();

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
                string fileName = $"Kvitto - {date}.txt";
                Directory.CreateDirectory($"../../../Receipts");
                File.AppendAllText($"../../../Receipts/{fileName}", receiptText);
                Console.WriteLine(receiptText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving receipt: {ex.Message}");
            }

        }
    }
}
