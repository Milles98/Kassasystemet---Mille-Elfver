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

                //om användaren skriver pay, string comparison så att användaren kan skriva pay eller PAY
                if (userInput.Equals("PAY", StringComparison.OrdinalIgnoreCase))
                {
                    //sparar kvittot och visar det
                    SavingAndDisplayingReceipt.SaveAndDisplayReceipt(receipt);
                    Console.WriteLine("\nKöpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");
                    break;
                }

                //om användaren skriver items, string comparison så att användaren kan skriva items hur den vill 
                if (userInput.Equals("ITEMS", StringComparison.OrdinalIgnoreCase))
                {
                    //Skriver ut listan på alla produkter
                    Console.Clear();
                    DisplayingProducts.DisplayTheProducts();
                    continue;
                }

                if (userInput.Equals("MENU", StringComparison.OrdinalIgnoreCase))
                {
                    Menu.MainMenu();
                }

                //split för produktid och antal
                string[] productParts = userInput.Split(' ');

                //felhantering om längden inte är ex 300 1
                if (productParts.Length != 2)
                {
                    Console.Clear();
                    Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                    DisplayingProducts.DisplayTheProducts();
                    continue;
                }

                string productId = productParts[0];
                int quantityOfProducts;

                //Felhantering om användaren inte börjar sin inmatning med produktID
                if (!int.TryParse(productParts[1], out quantityOfProducts))
                {
                    Console.WriteLine("Det här valet fanns inte, här är en lista för produkterna:\n");
                    DisplayingProducts.DisplayTheProducts();
                    continue;
                }

                //lägger till produkterna till kvittot
                AddingToReceipt.AddProductsToReceipt(productId, quantityOfProducts, ref receipt);

            }

            //return receipt om while loopen inte används
            return receipt;

        }
        public static void NewSaleMenu()
        {
            Console.WriteLine("Kommandon:");
            Console.WriteLine("<productid> <antal> <PAY> <ITEMS> <MENU>");
            Console.Write("Kommando: ");
        }
    }
}
