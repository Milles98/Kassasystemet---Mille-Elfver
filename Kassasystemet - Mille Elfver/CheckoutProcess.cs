using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public static class CheckoutProcess
    {

        /// <summary>
        /// Menu choices to add products, display items, pay for items and return to menu
        /// </summary>
        public static void ProcessCustomer(IProductServices productServices)
        {
            using (var container = AutoFacRegistration.RegisteredContainers())
            {
                ReceiptCreation receiptCreation = container.Resolve<ReceiptCreation>();
                IFileManager fileManager = container.Resolve<IFileManager>();

                while (true)
                {
                    productServices.DisplayAvailableProducts();
                    DisplayMenu();
                    string userInput = Console.ReadLine().Trim().ToUpper();

                    try
                    {
                        ProcessUserInput(userInput, productServices, receiptCreation, fileManager);
                    }
                    catch (FormatException ex)
                    {
                        ErrorMessage($"Fel vid inmatning: {ex.Message}");
                    }
                }

                static void ProcessUserInput(string userInput, IProductServices productServices, ReceiptCreation receiptCreation, IFileManager fileManager)
                {
                    if (userInput == "PAY")
                    {
                        Console.Clear();
                        if (receiptCreation.CartIsEmpty())
                        {
                            ErrorMessage("Kundvagnen är tom, lägg till lite produkter först.");
                            Thread.Sleep(1000);
                            return;
                        }

                        string receiptText = receiptCreation.CreateReceipt();
                        SaveReceipt(receiptText, fileManager);

                        MakeSound();

                        Console.ReadKey();

                        MainMenu.Menu(productServices);

                    }

                    if (userInput == "MENU")
                    {
                        MainMenu.Menu(productServices);
                    }

                    string[] productParts = userInput.Split(' ');
                    Product productID = productServices.GetProduct(productParts[0]);

                    if (productID == null)
                    {
                        ErrorMessage("Det här valet fanns inte, välj id och antal/kg enligt nedan (ex 300 1)");
                        return;
                    }

                    if (productParts.Length != 2 || !decimal.TryParse(productParts[1], out decimal quantity) || quantity > 50000)
                    {
                        ErrorMessage("Det här valet fanns inte, välj id och antal/kg enligt nedan (ex 300 1)");
                        return;
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

                static void SaveReceipt(string receiptText, IFileManager fileManager)
                {
                    fileManager.SaveReceipt(receiptText);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Köpet har genomförts och kvitto nedsparat. Tryck valfri knapp för att komma tillbaka till menyn");
                    Console.ResetColor();
                }

                /// <summary>
                /// Menu when user goes through option '1. Ny Kund'
                /// </summary>
                static void DisplayMenu()
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
}
