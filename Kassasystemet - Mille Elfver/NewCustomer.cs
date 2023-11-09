using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public static class NewCustomer
    {
        /// <summary>
        /// Menu choices to add products, display items, pay for items and return to menu
        /// </summary>
        public static void NewCustomerChoices(IProductService productServices)
        {
            ReceiptCreation receiptCreation = new ReceiptCreation(productServices);
            FileManager fileManager = new FileManager();

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
                        ErrorMessage("Kundvagnen är tom, lägg till lite produkter först.");
                        Thread.Sleep(1000);
                        continue;
                    }

                    PaymentSuccessful(receiptCreation, fileManager);

                    MakeSound();

                    break;
                }

                if (userInput == "MENU")
                {
                    MainMenu.Menu(productServices);
                }

                // splittar inmatningen index 0 (produktID) och index 1 (antal)
                string[] productParts = userInput.Split(' ');
                Product productID = productServices.GetProduct(productParts[0]);
                if (productID == null)
                {
                    ErrorMessage("\"Det här valet fanns inte, välj id och antal/kg enligt nedan (ex 300 1)\"");
                    continue;
                }
                if (productParts.Length != 2 || !decimal.TryParse(productParts[1], out decimal quantity) || quantity > 50000)
                {
                    ErrorMessage("\"Det här valet fanns inte, välj id och antal/kg enligt nedan (ex 300 1)\"");
                    continue;
                }

                receiptCreation.AddingToReceipt(productID, quantity);
                Thread.Sleep(1000);
            }

            static void MakeSound()
            {
                string soundFilePath = "../../../Kvittoljud/KACHING.wav";
                SoundPlayer soundPlayer = new SoundPlayer(soundFilePath);
                soundPlayer.Load();
                soundPlayer.Play();
            }

            static void PaymentSuccessful(ReceiptCreation receiptCreation, FileManager fileManager)
            {
                string receiptText = receiptCreation.CreateReceipt();
                fileManager.SaveReceipt(receiptText);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Köpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");
                Console.ResetColor();
            }

            /// <summary>
            /// Menu when user goes through option '1. Ny Kund'
            /// </summary>
            static void NewCustomerMenu()
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("╭──────────────────────────────────╮");
                Console.WriteLine("│KASSA                             │");
                Console.WriteLine("│Kommandon:                        │");
                Console.WriteLine("│<productid> <antal> <PAY> <MENU>  │");
                Console.WriteLine("│Exempel: 300 mellanslag 1,5       │");
                Console.WriteLine("╰──────────────────────────────────╯");
                Console.ResetColor();
                Console.Write("Kommando: ");
            }

            static void ErrorMessage(string message)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
    }
}
