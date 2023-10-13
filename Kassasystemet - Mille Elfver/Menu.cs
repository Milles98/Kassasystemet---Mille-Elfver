using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Menu
    {
        /// <summary>
        /// Shows the main menu to user
        /// </summary>
        public static void MainMenu(ProductCatalog productCatalog)
        {
            
            bool programRunning = true;
            do
            {
                //Menylista för kassasystemet 
                Console.Clear();
                Console.WriteLine(" ------------");
                Console.WriteLine("| KASSA      |");
                Console.WriteLine("| 1. Ny kund |");
                Console.WriteLine("| 2. Admin   |");
                Console.WriteLine("| 0. Avsluta |");
                Console.WriteLine(" ------------");
                Console.Write("Inmatning: ");

                int val;
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out val) && val >= 0 && val <= 2) //try parse istället för try catch
                {
                    switch (val)
                    {
                        case 1:
                            Console.Clear();
                            NewCustomer.NewCustomerChoices(productCatalog);
                            break;

                        case 2:
                            Admin.AdminTools(productCatalog);
                            break;

                        case 0:
                            productCatalog.SaveProductsToFile();
                            Console.WriteLine("Tryck valfri knapp för att avsluta programmet.");
                            programRunning = false;
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltig inmatning, tryck valfri knapp för att återgå till menyn och välj '1', '2' eller '0'");
                }
                Console.ReadKey();

            } while (programRunning);
        }
    }
}
