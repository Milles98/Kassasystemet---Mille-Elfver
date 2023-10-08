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
        private int receiptCounter = 1337;

        public ShoppingCart()
        {
            LoadReceiptCounter();
        }

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
                    productInfo = $"{product.Name.PadRight(15)} {quantity:F2} st * {product.UnitPrice:F2}";
                }

                int paddingSpaces = Math.Max(0, 45 - productInfo.Length - totalPrice.ToString("F2").Length);

                string productToAdd = $"{productInfo}{new string(' ', paddingSpaces)}{totalPrice:F2}";

                receipt.AppendLine(productToAdd);

                string priceType = product.IsKiloPrice ? "kilo" : "st"; //ternary istället för if-sats

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
        public string CreateReceipt()
        {
            decimal totalAmount = CalculateTotal();

            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            string receiptSeparator = new string('-', 45);
            int maxWidth = 45;
            int availableSpace = maxWidth - "Total:".Length;
            string totalAmountText = totalAmount.ToString("C");
            string totalAmountPadding = new string(' ', availableSpace - totalAmountText.Length);
            string totalLine = $"\nTOTAL:{totalAmountPadding}{totalAmountText}";

            StringBuilder receiptText = new StringBuilder();
            receiptText.AppendLine(receiptSeparator);
            receiptText.AppendLine($"KVITTO NR: {receiptCounter}{formattedDate.PadLeft(30)}\n");
            receiptText.AppendLine($"Milles Butik ( +46 123 456 789 )");
            receiptText.AppendLine($"Milles Butik AB");
            receiptText.AppendLine($"Årstaängsvägen 31, 117 43 Stockholm");
            receiptText.AppendLine($"Organisations Nr: 55421-7125\n");

            receiptText.Append(receipt.ToString());

            receiptText.AppendLine($"\nTELLER SE / NO");
            receiptText.AppendLine($"BUTIKSNR: 76091234");
            receiptText.AppendLine($"TERM: 11477096-835026\n");

            receiptText.AppendLine($"MasterCard");
            receiptText.AppendLine($"Contactless");
            receiptText.AppendLine($"************1234-1");
            receiptText.AppendLine($"AID: A00000000030910");
            receiptText.AppendLine($"TVR: 000000007101");
            receiptText.AppendLine($"REF: 290643 480957 KC1");
            receiptText.AppendLine($"RESP: 00");
            receiptText.AppendLine($"PERIOD: 417");

            receiptText.AppendLine($"\nKÖP");
            receiptText.AppendLine($"GODKÄNT");

            receiptText.AppendLine($"MOMS Punkt: 745201");
            receiptText.AppendLine($"6B KVITTO NR: {receiptCounter}\n");

            receiptText.AppendLine($"Din Kassör Idag Var Mille");
            receiptText.AppendLine($"Tack Och Välkommen Åter!");

            receiptText.AppendLine(totalLine);
            receiptText.AppendLine(receiptSeparator);

            receiptCounter++;
            SaveReceiptCounter();

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
                string fileName = $"Kvitton - {date}.txt";
                Directory.CreateDirectory($"../../../Kvitton");
                File.AppendAllText($"../../../Kvitton/{fileName}", receiptText);
                Console.WriteLine(receiptText);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparandet av kvitto: {ex.Message}");
            }

        }

        /// <summary>
        /// Loads the last receipt number from "KvittoRäknare" textfile
        /// </summary>
        private void LoadReceiptCounter()
        {
            try
            {
                if (File.Exists("../../../Kvitton/KvittoRäknare.txt"))
                {
                    string counterText = File.ReadAllText("../../../Kvitton/KvittoRäknare.txt");
                    if (int.TryParse(counterText, out int counter))
                    {
                        receiptCounter = counter;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid laddandet av kvittoräknare: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves the last receipt number to "KvittoRäknare" textfile
        /// </summary>
        private void SaveReceiptCounter()
        {
            try
            {
                Directory.CreateDirectory($"../../../Kvitton");
                File.WriteAllText("../../../Kvitton/KvittoRäknare.txt", receiptCounter.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparandet av kvittoräknare: {ex.Message}");
            }
        }
    }
}
