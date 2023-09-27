﻿using System;
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
        public static void AddProductsToReceipt(string productId, int quantityOfProducts, ref string receipt)
        {
            Console.Clear();
            if (Product.availableProducts.TryGetValue(productId, out Product selectedProduct))
            {
                if (quantityOfProducts < 1)
                {
                    Console.WriteLine("Du kan inte ange mindre än 1 i antal!");
                }
                else
                {
                    decimal totalPrice = selectedProduct.Price * quantityOfProducts;
                    //denna del läggs på kvittot i följd "produktnamn - antal produkter * priset = totalen
                    string productToAdd = $"{selectedProduct.Name.PadRight(15)} {quantityOfProducts} * {selectedProduct.Price}, {selectedProduct.PriceType} {totalPrice,14}";
                    receipt += productToAdd + "\n";

                    //Skriver ut vad som lagts till i kassan
                    Console.WriteLine($"Produkt: {selectedProduct.Name}, {quantityOfProducts} st, totalt pris: {totalPrice} kr har lagts till på kvittot.");
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
