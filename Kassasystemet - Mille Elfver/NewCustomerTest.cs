using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class NewCustomerTest
    {
        public static void NewCustomerChoices()
        {
            ProductCatalog productCatalog = new ProductCatalog();
            ShoppingCart shoppingCart = new ShoppingCart();

            while (true)
            {
                NewCustomerMenu();
                string userInput = Console.ReadLine().Trim();

                if (userInput.Equals("PAY", StringComparison.OrdinalIgnoreCase))
                {
                    string receiptText = shoppingCart.CreateReceipt();
                    shoppingCart.SaveReceipt(receiptText);

                    Console.WriteLine("Köpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");
                    break;
                }

                if (userInput.Equals("ITEMS", StringComparison.OrdinalIgnoreCase))
                {
                    productCatalog.DisplayAvailableProducts();
                    continue;
                }

                if (userInput.Equals("MENU", StringComparison.OrdinalIgnoreCase))
                {
                    Menu.MainMenu();
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
            /// Menu when user goes through option '1'
            /// </summary>
            static void NewCustomerMenu()
            {
                Console.WriteLine("KASSA");
                Console.WriteLine("Kommandon:");
                Console.WriteLine("<productid> <antal> <PAY> <ITEMS> <MENU>");
                //Console.WriteLine("Exempel: 300 1 = Banan 1kg, 308 0,5 = Tomater 0,5kg");
                Console.Write("Kommando: ");
            }
        }
    }
}
