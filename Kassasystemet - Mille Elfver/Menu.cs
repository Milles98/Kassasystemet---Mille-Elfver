using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Kassasystemet___Mille_Elfver
{
    public class Menu
    {
        /// <summary>
        /// Displays main menu to user
        /// </summary>
        public static void MainMenu(ProductServices productServices)
        {
            bool programRunning = true;
            do
            {
                Console.Clear();
                Console.WriteLine("╭─────────────╮");
                Console.WriteLine("│ KASSA       │");
                Console.WriteLine("│ 1. Ny kund  │");
                Console.WriteLine("│ 2. Admin    │");
                Console.WriteLine("│ 3. Historik │");
                Console.WriteLine("│ 0. Avsluta  │");
                Console.WriteLine("╰─────────────╯");
                Console.Write(" Inmatning: ");

                int val;
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out val) && val >= 0 && val <= 3)
                {
                    switch (val)
                    {
                        case 1:
                            Console.Clear();
                            NewCustomer.NewCustomerChoices(productServices);
                            break;

                        case 2:
                            Admin.AdminTools(productServices);
                            break;

                        case 3:
                            Console.Clear();
                            Console.WriteLine("╭───────────────────────────────────────────────────╮");
                            Console.WriteLine("│ Historik för dagens kvitton... Tryck valfri knapp │");
                            Console.WriteLine("╰───────────────────────────────────────────────────╯");
                            Console.ReadKey();

                            var date = DateTime.Now.ToShortDateString();
                            var filePath = $"../../../Kvitton/Kvitton - {date}.txt";

                            if (File.Exists(filePath))
                            {
                                Console.Clear();
                                var allReceipts = File.ReadAllLines(filePath);

                                foreach (var receipt in allReceipts)
                                {
                                    Console.WriteLine(receipt);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Dagens kvittohistorik fil är tom/saknas (Har du sålt något ännu?).");
                            }
                            Console.WriteLine("\nEnter för att gå tillbaka till huvudmenyn");
                            break;

                        case 0:
                            productServices.SaveProductsToFile();
                            Console.WriteLine("Tryck valfri knapp för att avsluta programmet.");
                            programRunning = false;
                            Environment.Exit(0);
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltig inmatning, tryck valfri knapp för att återgå till menyn och välj '1', '2', '3' eller '0'");
                }
                Console.ReadKey();

            } while (programRunning);
        }
    }
}
