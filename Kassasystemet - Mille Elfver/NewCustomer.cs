using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class NewCustomer
    {
        /// <summary>
        /// Menu choices to add products, display items, pay for items and return to menu
        /// </summary>
        public static void NewCustomerChoices(ProductServices productServices)
        {
            ReceiptCreation receiptCreation = new ReceiptCreation(productServices);

            while (true)
            {
                productServices.DisplayAvailableProducts();
                NewCustomerMenu();
                string userInput = Console.ReadLine().Trim().ToUpper();

                if (userInput == "PAY")
                {
                    Console.Clear();
                    if (receiptCreation.CartIsEmpty())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Kundvagnen är tom, lägg till lite produkter först.");
                        Console.WriteLine("Valfri knapp, gå tillbaka och gör om!");
                        Console.ResetColor();
                        Console.ReadKey();
                        continue;
                    }
                    string receiptText = receiptCreation.CreateReceipt();
                    receiptCreation.SaveReceipt(receiptText);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Köpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");
                    Console.ResetColor();

                    string soundFilePath = "../../../Kvittoljud/KACHING.wav";
                    SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
                    soundPlayer.Load();
                    soundPlayer.Play();

                    break;
                }

                if (userInput == "MENU")
                {
                    Menu.MainMenu(productServices);
                }

                string[] productParts = userInput.Split(' ');
                if (productParts.Length != 2 || !decimal.TryParse(productParts[1], out decimal quantity))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Det här valet fanns inte, välj id och antal/kg enligt nedan (ex 300 1)");
                    Console.ResetColor();
                    continue;
                }

                Product product = productServices.GetProduct(productParts[0]);
                if (product == null)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Det här valet fanns inte, välj id och antal/kg enligt nedan (ex 300 1");
                    Console.ResetColor();
                    continue;
                }

                receiptCreation.AddingToReceipt(product, quantity);
                Thread.Sleep(1000);
            }

            /// <summary>
            /// Menu when user goes through option '1. Ny Kund'
            /// </summary>
            static void NewCustomerMenu()
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╭──────────────────────────────────╮");
                Console.WriteLine("│KASSA                             │");
                Console.WriteLine("│                                  │");
                Console.WriteLine("│Kommandon:                        │");
                Console.WriteLine("│<productid> <antal> <PAY> <MENU>  │");
                Console.WriteLine("│                                  │");
                Console.WriteLine("│Exempel: 300 1,5 = Bananer 1,5kg  │");
                Console.WriteLine("╰──────────────────────────────────╯");
                Console.ResetColor();
                Console.Write("Kommando: ");
            }
        }
    }
}
