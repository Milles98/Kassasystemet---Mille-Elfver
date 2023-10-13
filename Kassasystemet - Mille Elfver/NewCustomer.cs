using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class NewCustomer
    {
        /// <summary>
        /// Menu choices to add products, display items, pay for items and return to menu
        /// </summary>
        public static void NewCustomerChoices(ProductCatalog productCatalog)
        {
            ShoppingCart shoppingCart = new ShoppingCart(productCatalog);

            while (true)
            {
                NewCustomerMenu();
                string userInput = Console.ReadLine().Trim();

                if (userInput.Equals("PAY", StringComparison.OrdinalIgnoreCase))
                {
                    if (shoppingCart.IsCartEmpty())
                    {
                        Console.WriteLine("Kundvagnen är tom, lägg till lite produkter först.");
                        return;
                    }
                    Console.Clear();
                    string receiptText = shoppingCart.CreateReceipt();
                    shoppingCart.SaveReceipt(receiptText);
                    Console.WriteLine("Köpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");
                    break;
                }

                if (userInput.Equals("ITEMS", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    productCatalog.DisplayAvailableProducts();
                    continue;
                }

                if (userInput.Equals("MENU", StringComparison.OrdinalIgnoreCase))
                {
                    Menu.MainMenu(productCatalog);
                }
                
                if (userInput.Equals("HISTORIK", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    var date = DateTime.Now.ToShortDateString();
                    var allReceipts = File.ReadAllLines($"../../../Kvitton/Kvitton - {date}.txt");
                    foreach (var receipt in allReceipts)
                    {
                        Console.WriteLine(receipt);
                    }
                    continue;
                }

                string[] productParts = userInput.Split(' ');
                if (productParts.Length != 2 || !decimal.TryParse(productParts[1], out decimal quantity))
                {
                    Console.Clear();
                    Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                    productCatalog.DisplayAvailableProducts();
                    continue;
                }

                Product product = productCatalog.GetProduct(productParts[0]);
                if (product == null)
                {
                    Console.Clear();
                    Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                    productCatalog.DisplayAvailableProducts();
                    continue;
                }

                shoppingCart.AddingToReceipt(product, quantity);
            }

            /// <summary>
            /// Menu when user goes through option '1. Ny Kund'
            /// </summary>
            static void NewCustomerMenu()
            {
                Console.WriteLine(" -----------------------------------------------------");
                Console.WriteLine("| KASSA                                               |");
                Console.WriteLine("| Kommandon:                                          |");
                Console.WriteLine("| <productid> <antal> <PAY> <ITEMS> <MENU> <HISTORIK> |");
                Console.WriteLine(" -----------------------------------------------------");
                Console.Write("Kommando: ");
            }
        }
    }
}
