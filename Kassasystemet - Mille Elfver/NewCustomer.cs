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
        /// Starts a new sale for user to add products etc
        /// </summary>
        /// <returns></returns>
        public static string NewCustomerChoices()
        {
            //deklarerar variabeln receipt som används till att hålla informationen under försäljningen
            string receipt = "";

            while (true)
            {
                NewCustomerMenu();
                string userInput = Console.ReadLine().Trim();

                string[] productParts = userInput.Split(' '); //splittar bort mellanslagen i userInput, delimitern är mellanslag
                string productId = productParts[0]; //productID väljer jag att det är på index 0 (pga dictionary)
                decimal quantityOfProducts; //kommer att spara antal från användaren

                if (userInput.Equals("PAY", StringComparison.OrdinalIgnoreCase))
                {
                    //sparar kvittot och visar det
                    SavingReceipt.Receipt(receipt);
                    Console.WriteLine("\nKöpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");
                    break;
                }

                if (userInput.Equals("ITEMS", StringComparison.OrdinalIgnoreCase))
                {
                    Products.DisplayTheProducts(); //Skriver ut listan på alla produkter
                    continue;
                }

                if (userInput.Equals("MENU", StringComparison.OrdinalIgnoreCase))
                {
                    Menu.MainMenu();
                }

                if (productParts.Length != 2) //felhantering om längden inte är ex 300 1
                {
                    Console.Clear();
                    Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                    Products.DisplayTheProducts();
                    continue;
                }

                if (!decimal.TryParse(productParts[1], out quantityOfProducts)) //kollar om quantityofproducts inte är på index 1, då skrivs följande:
                {
                    Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                    Products.DisplayTheProducts();
                    continue;
                }

                //lägger till produkterna till kvittot om användarens formattering är korrekt 
                AddingToReceipt.AddProductsToReceipt(productId, quantityOfProducts, ref receipt);

            }

            return receipt; // efter att användaren köpt klart genom "PAY" returneras receipt som innehåller all köp info

        }
        /// <summary>
        /// Menu when user goes through option '1'
        /// </summary>
        public static void NewCustomerMenu()
        {
            Console.WriteLine("KASSA");
            Console.WriteLine("Kommandon:");
            Console.WriteLine("<productid> <antal> <PAY> <ITEMS> <MENU>");
            Console.Write("Kommando: ");
        }
    }
}
