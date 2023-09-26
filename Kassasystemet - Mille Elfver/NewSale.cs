using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class NewSale
    {
        public static string NewSales()
        {
            //deklarerar variabeln receipt som sedan kommer tilldelas info från SaveAndDisplayReceipt
            string receipt = "";

            while (true)
            {
                NewSaleMenu();
                string userInput = Console.ReadLine().Trim();

                if (userInput.Equals("PAY", StringComparison.OrdinalIgnoreCase))
                {
                    //sparar kvittot och visar det
                    SavingAndDisplayingReceipt.SaveAndDisplayReceipt(receipt);
                    Console.WriteLine("\nKöpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");
                    break;
                }

                if (userInput.Equals("ITEMS", StringComparison.OrdinalIgnoreCase))
                {
                    DisplayingProducts.DisplayTheProducts(); //Skriver ut listan på alla produkter
                    continue;
                }

                if (userInput.Equals("MENU", StringComparison.OrdinalIgnoreCase))
                {
                    Menu.MainMenu();
                }

                string[] productParts = userInput.Split(' '); //splittar bort mellanslagen i userInput
                string productId = productParts[0]; //productID väljer jag att det är på index 0 (pga dictionary)
                int quantityOfProducts; //denna väljer jag är index 1 kolla rad 52

                if (productParts.Length != 2) //felhantering om längden inte är ex 300 1
                {
                    Console.Clear();
                    Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                    DisplayingProducts.DisplayTheProducts();
                    continue;
                }

                if (!int.TryParse(productParts[1], out quantityOfProducts)) //kollar om quantityofproducts inte är på index 1, då skrivs följande:
                {
                    Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                    DisplayingProducts.DisplayTheProducts();
                    continue;
                }

                //lägger till produkterna till kvittot
                AddingToReceipt.AddProductsToReceipt(productId, quantityOfProducts, ref receipt);

            }

            return receipt; //return receipt efter att den har gått igenom while-loopens val

        }
        public static void NewSaleMenu()
        {
            Console.WriteLine("KASSA");
            Console.WriteLine("Kommandon:");
            Console.WriteLine("<productid> <antal> <PAY> <ITEMS> <MENU>");
            Console.Write("Kommando: ");
        }
    }
}
