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
                string userInput = Console.ReadLine().Trim();

                if (userInput.Equals("PAY", StringComparison.OrdinalIgnoreCase))
                {
                    Console.Clear();
                    if (receiptCreation.CartIsEmpty())
                    {
                        Console.WriteLine("Kundvagnen är tom, lägg till lite produkter först.");
                        return;
                    }
                    string receiptText = receiptCreation.CreateReceipt();
                    receiptCreation.SaveReceipt(receiptText);
                    Console.WriteLine("Köpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");

                    string soundFilePath = "../../../Kvittoljud/KACHING.wav";
                    SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
                    soundPlayer.Load();
                    soundPlayer.Play();

                    break;
                }

                if (userInput.Equals("MENU", StringComparison.OrdinalIgnoreCase))
                {
                    Menu.MainMenu(productServices);
                }

                string[] productParts = userInput.Split(' ');
                if (productParts.Length != 2 || !decimal.TryParse(productParts[1], out decimal quantity))
                {
                    Console.Clear();
                    Console.WriteLine("Det här valet fanns inte, välj id och antal/kg enligt nedan (ex 300 1)");
                    continue;
                }

                Product product = productServices.GetProduct(productParts[0]);
                if (product == null)
                {
                    Console.Clear();
                    Console.WriteLine("Det här valet fanns inte, välj id och antal/kg enligt nedan (ex 300 1");
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
                Console.WriteLine("╭──────────────────────────────────╮");
                Console.WriteLine("│ KASSA                            │");
                Console.WriteLine("│ Kommandon:                       │");
                Console.WriteLine("│ <productid> <antal> <PAY> <MENU> │");
                Console.WriteLine("╰──────────────────────────────────╯");
                Console.Write("Kommando: ");
            }
        }
    }
}
