using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ReceiptCreation
    {
        private ProductServices _productServices;
        private StringBuilder _receipt = new StringBuilder();
        private ReceiptCounter _receiptCounter = new ReceiptCounter();
        private bool _cartIsEmpty = true;

        public ReceiptCreation(ProductServices productServices)
        {
            _productServices = productServices;
            _receiptCounter.LoadReceiptCounter();
        }

        /// <summary>
        /// Adds the productID and amount to receipt StringBuilder
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        public void AddingToReceipt(Product product, decimal quantity)
        {
            Console.Clear();
            if (product != null)
            {
                _cartIsEmpty = false;
                decimal totalPrice;
                string productInfo;

                if (product.IsKiloPrice)
                {
                    if (quantity > 0)
                    {
                        //kollar kilopris köp x betala för y uträkning o lägger in på totalPrice variabeln
                        //if (product.Discounts.BuyQuantity > 0 && product.Discounts.PayForQuantity > 0 &&
                        //    DateTime.Now >= product.Discounts.BuyQuantityStartDate && DateTime.Now <= product.Discounts.BuyQuantityEndDate)
                        //{
                        //    int buyQuantity = product.Discounts.BuyQuantity;
                        //    int payForQuantity = product.Discounts.PayForQuantity;

                        //    int discountedQuantity = (int)(quantity / buyQuantity) * payForQuantity + (int)(quantity % buyQuantity);
                        //    decimal discountedPrice = product.KiloPrice * discountedQuantity;
                        //    totalPrice = discountedPrice;

                        //    productInfo = $"{product.Name.PadRight(20)} {quantity:F2} kg * {product.KiloPrice:F2} " +
                        //        $"(Mängdrabatt {buyQuantity} för {payForQuantity})";

                        //    //kollar om kilopriset även har en procentrabatt och lägger in den uträkningen i totalPrice
                        //    if (product.Discounts.PercentageDiscount > 0 && DateTime.Now >= product.Discounts.PercentStartDate &&
                        //        DateTime.Now <= product.Discounts.PercentEndDate)
                        //    {
                        //        decimal percentDiscountedPrice = discountedPrice - (discountedPrice * (product.Discounts.PercentageDiscount / 100));
                        //        totalPrice = percentDiscountedPrice;
                        //        productInfo += $" + ({product.Discounts.PercentageDiscount:F0}%)";
                        //    }
                        //}

                        //kollar kilopris procentrabatt uträkning o lägger in på totalPrice variabeln
                        if (product.Discounts.PercentageDiscount > 0 && DateTime.Now >= product.Discounts.PercentStartDate &&
                            DateTime.Now <= product.Discounts.PercentEndDate)
                        {
                            decimal discountedPrice = (product.KiloPrice - (product.KiloPrice * (product.Discounts.PercentageDiscount / 100))) * quantity;
                            totalPrice = discountedPrice;
                            productInfo = $"{product.Name.PadRight(20)} {quantity:F2} kg * {product.KiloPrice:F2} " +
                                $"(Rabatt {product.Discounts.PercentageDiscount:F0}%)";
                        }
                        //kollar kilopris uträkning o lägger in på totalPrice variabeln
                        else
                        {
                            totalPrice = product.KiloPrice * quantity;
                            productInfo = $"{product.Name.PadRight(20)} {quantity:F2} kg * {product.KiloPrice:F2}";
                        }
                    }

                    else
                    {
                        ErrorMessage("Ogiltig inmatning, du kan inte ange 0 eller mindre.");
                        return;
                    }

                }

                else
                {

                    if (quantity > 0 && quantity == (int)quantity)
                    {
                        //kollar styckpris köp x betala för y uträkning o lägger in på totalPrice variabeln
                        if (product.Discounts.BuyQuantity > 0 && product.Discounts.PayForQuantity > 0 &&
                            DateTime.Now >= product.Discounts.BuyQuantityStartDate && DateTime.Now <= product.Discounts.BuyQuantityEndDate)
                        {
                            int buyQuantity = product.Discounts.BuyQuantity;
                            int payForQuantity = product.Discounts.PayForQuantity;

                            int discountedQuantity = (int)(quantity / buyQuantity) * payForQuantity + (int)(quantity % buyQuantity);
                            decimal discountedPrice = product.UnitPrice * discountedQuantity;
                            totalPrice = discountedPrice;

                            productInfo = $"{product.Name.PadRight(20)} {quantity:F0} st * {product.UnitPrice:F2} ({buyQuantity} för {payForQuantity})";

                            //kollar om styckpriset även har en procentrabatt och lägger in den uträkningen i totalPrice
                            if (product.Discounts.PercentageDiscount > 0 && DateTime.Now >= product.Discounts.PercentStartDate &&
                                DateTime.Now <= product.Discounts.PercentEndDate)
                            {
                                decimal percentDiscountedPrice = discountedPrice - (discountedPrice * (product.Discounts.PercentageDiscount / 100));
                                totalPrice = percentDiscountedPrice;
                                productInfo += $" + ({product.Discounts.PercentageDiscount:F0}%)";
                            }

                        }
                        //kollar styckpris procentrabatt uträkning o lägger in på totalPrice variabeln
                        else if (product.Discounts.PercentageDiscount > 0 && DateTime.Now >= product.Discounts.PercentStartDate &&
                            DateTime.Now <= product.Discounts.PercentEndDate)
                        {
                            decimal discountedPrice = (product.UnitPrice - (product.UnitPrice * (product.Discounts.PercentageDiscount / 100))) * quantity;
                            totalPrice = discountedPrice;
                            productInfo = $"{product.Name.PadRight(20)} {quantity:F0} st * {product.UnitPrice:F2} " +
                                $"(Rabatt {product.Discounts.PercentageDiscount:F0}%)";
                        }
                        //kollar styckpris uträkning o lägger in på totalPrice variabeln
                        else
                        {
                            totalPrice = product.UnitPrice * quantity;
                            productInfo = $"{product.Name.PadRight(20)} {quantity:F0} st * {product.UnitPrice:F2}";
                        }

                    }

                    else
                    {
                        ErrorMessage("Ogiltig inmatning, mängden behöver vara mer än 0 och ett heltal!");
                        return;
                    }

                }

                int paddingSpaces = Math.Max(0, 70 - productInfo.Length - totalPrice.ToString("F2").Length);

                string productToAdd = $"{productInfo}{new string(' ', paddingSpaces)}{totalPrice:F2}";

                _receipt.AppendLine(productToAdd);

                string priceType = product.IsKiloPrice ? "kilo" : "st"; //ternary istället för if-sats

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Produkt: {product.Name}, {quantity} {priceType:F2}, totalt pris: {totalPrice:F2} kr har lagts till på kvittot.");
                Console.ResetColor();
            }

            else
            {
                ErrorMessage("Det här valet fanns inte, här är en lista för produkterna:\n");
                _productServices.DisplayAvailableProducts();
            }
        }

        /// <summary>
        /// Returns true (cart is indeed empty) if no products has been added to the receipt
        /// </summary>
        /// <returns></returns>
        public bool CartIsEmpty()
        {
            return _cartIsEmpty;
        }

        /// <summary>
        /// Calculates all the products added to receipt StringBuilder
        /// </summary>
        /// <returns></returns>
        public decimal CalculateReceiptTotal()
        {

            string[] linesInReceipt = _receipt.ToString().Split('\n');

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
            Console.Clear();
            decimal totalAmount = CalculateReceiptTotal();
            int receiptNumber = _receiptCounter.GetReceiptNumber();

            DateTime dateTime = DateTime.Now;
            string formattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            string receiptSeparator = new string('-', 70);
            int maxWidth = 70;
            int availableSpace = maxWidth - "Total:".Length;
            string totalAmountText = totalAmount.ToString("C");
            string totalAmountPadding = new string(' ', availableSpace - totalAmountText.Length);
            string totalLine = $"\nTOTAL:{totalAmountPadding}{totalAmountText}";


            StringBuilder receiptText = new StringBuilder();

            receiptText.AppendLine(receiptSeparator);
            receiptText.AppendLine($"KVITTO NR: {receiptNumber}{formattedDate.PadLeft(55)}\n");
            receiptText.AppendLine($"Milles Butik AB ( +46 123 456 789 )");
            receiptText.AppendLine($"Årstaängsvägen 31, 117 43 Stockholm");
            receiptText.AppendLine($"Organisations Nr: 55123-1234\n");

            receiptText.Append(_receipt.ToString());

            receiptText.AppendLine($"\nTELLER SE / NO");
            receiptText.AppendLine($"BUTIKSNR: 12345678");
            receiptText.AppendLine($"TERM: 12345678-123456\n");

            receiptText.AppendLine($"MasterCard");
            receiptText.AppendLine($"Contactless");
            receiptText.AppendLine($"************1234-5");
            receiptText.AppendLine($"AID: A00000000012345");
            receiptText.AppendLine($"TVR: 000000001234");
            receiptText.AppendLine($"REF: 123456 123456 KC1");
            receiptText.AppendLine($"RESP: 12");
            receiptText.AppendLine($"PERIOD: 417");

            receiptText.AppendLine($"\nKÖP GODKÄNT");

            receiptText.AppendLine($"MOMS Punkt: 123456");
            receiptText.AppendLine($"6B KVITTO NR: {receiptNumber}\n");

            receiptText.AppendLine($"Din Kassör Idag Var Mille");
            receiptText.AppendLine($"Tack Och Välkommen Åter!");

            receiptText.AppendLine(totalLine);
            receiptText.AppendLine(receiptSeparator);

            _receiptCounter.SaveReceiptCounter();
            _receiptCounter.IncreaseCounter();

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
                ErrorMessage($"Fel vid sparandet av kvitto: {ex.Message}");
            }
        }

        private void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
