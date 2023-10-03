using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class AddingToReceipt
    {
        /// <summary>
        /// Saves product input, returns productID, quantity and receipt
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantityOfProducts"></param>
        /// <param name="receipt"></param>

        //static och ref receipt så att ändringar gjorda på variabeln inuti metoden även visas utanför metoden
        public static void AddProductsToReceipt(string productId, decimal quantityOfProducts, ref string receipt)
        {
            Console.Clear();
            //hämtar produkt från product dictionary, om produkten hittas sparas det i selectedProduct variabeln
            if (Product.availableProducts.TryGetValue(productId, out Product selectedProduct))
            {
                if (quantityOfProducts <= 0)
                {
                    Console.WriteLine("Du kan inte ange mindre än 0,01 i antal!");
                }

                else
                {
                    decimal totalPrice;
                    string productInfo;

                    if (selectedProduct.IsKiloPrice)
                    {
                        totalPrice = selectedProduct.KiloPrice * quantityOfProducts;
                        productInfo = $"{selectedProduct.Name.PadRight(15)} {quantityOfProducts:F2} kg * {selectedProduct.KiloPrice:F2}";
                    }
                    else
                    {
                        totalPrice = selectedProduct.UnitPrice * quantityOfProducts;
                        productInfo = $"{selectedProduct.Name.PadRight(15)} {quantityOfProducts} st * {selectedProduct.UnitPrice:F2}";
                    }

                    int paddingSpaces = Math.Max(0, 40 - productInfo.Length - totalPrice.ToString("F2").Length);

                    string productToAdd = $"{productInfo}{new string(' ', paddingSpaces)}{totalPrice:F2}";

                    receipt += productToAdd + "\n";

                    string priceType = selectedProduct.IsKiloPrice ? "kilo" : "st"; //smidigare lösning på en IF sats

                    //    //Skriver ut vad som lagts till i kassan
                    Console.WriteLine($"Produkt: {selectedProduct.Name}, {quantityOfProducts} {priceType:F2}, totalt pris: {totalPrice:F2} kr har lagts till på kvittot.");
                }
            }
            else
            {
                Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                Product.DisplayTheProducts();
            }
        }
    }
}
