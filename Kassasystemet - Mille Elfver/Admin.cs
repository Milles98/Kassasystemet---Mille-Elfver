using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public static class Admin
    {
        public static void AdminTools(ProductServices productServices)
        {
            bool adminRunning = true;
            do
            {
                AdminMenu();
                int userInput;
                if (!int.TryParse(Console.ReadLine(), out userInput))
                {
                    ErrorMessage($"\nOgiltig inmatning, valfri knapp försök igen.");
                    continue;
                }

                switch (userInput)
                {
                    case 1:
                        while (true)
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("╭────────────────────────────╮");
                            Console.WriteLine("│Ändra namn på produkt       │");
                            Console.WriteLine("│Q = tillbaka till adminmenyn│");
                            Console.WriteLine("╰────────────────────────────╯");
                            Console.ResetColor();

                            string productIdCase1 = GetUserInput("Ange 'Q' eller produkt-ID: ").ToUpper();

                            if (productIdCase1 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase1))
                            {
                                ErrorMessage($"Produkt med ID {productIdCase1} finns ej.");
                                continue;
                            }

                            string newName = GetUserInput("Ange det nya namnet: ");

                            if (newName.Length > 20 || newName.Length < 1)
                            {
                                ErrorMessage($"Ogiltigt produkt namn. Ange något mellan 1-20 bokstäver.");
                                continue;
                            }

                            productServices.UpdateProductName(productIdCase1, newName);

                            BackToAdminMenuMsg();
                            break;
                        }
                        break;

                    case 2:
                        while (true)
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();
                            decimal newPrice = 0;

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("╭────────────────────────────╮");
                            Console.WriteLine("│Ändra pris på produkter     │");
                            Console.WriteLine("│Q = tillbaka till adminmenyn│");
                            Console.WriteLine("╰────────────────────────────╯");
                            Console.ResetColor();

                            string productIdCase2 = GetUserInput("Ange 'Q' eller produkt-ID: ").ToUpper();

                            if (productIdCase2 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase2))
                            {
                                ErrorMessage($"Produkt med ID {productIdCase2} finns ej.");
                                continue;
                            }

                            string priceTypeChoice = GetUserInput("Vill du ändra till styckpris eller kilopris (S/K)? ").ToLower();

                            if (priceTypeChoice == "s" || priceTypeChoice == "k")
                            {
                                Console.Write($"Ange nytt pris per {priceTypeChoice}: ");
                                if (decimal.TryParse(Console.ReadLine().Trim(), out newPrice))
                                {
                                    if (newPrice > 0 && newPrice <= 50000) // Kontrollera om priset är inom intervallet
                                    {
                                        productServices.UpdateProductPrice(productIdCase2, newPrice, priceTypeChoice);
                                        BackToAdminMenuMsg();
                                        break;
                                    }
                                    else
                                    {
                                        ErrorMessage("Priset måste vara mellan 1 kr och 50 000 kr.");
                                    }
                                }
                                else
                                {
                                    ErrorMessage("Ogiltig inmatning. Ange ett giltigt pris.");
                                }
                            }
                            else
                            {
                                ErrorMessage($"Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
                                continue;
                            }
                        }
                        break;

                    case 3:
                        while (true)
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("╭────────────────────────────╮");
                            Console.WriteLine("│Lägg till ny produkt        │");
                            Console.WriteLine("│Q = tillbaka till adminmenyn│");
                            Console.WriteLine("╰────────────────────────────╯");
                            Console.ResetColor();

                            string productIdCase3 = GetUserInput("Ange 'Q' eller produktens ID: ").ToUpper();

                            if (productIdCase3 == "Q")
                            {
                                break;
                            }

                            if (productServices.ProductExists(productIdCase3))
                            {
                                ErrorMessage($"Produkt med ID {productIdCase3} finns redan.");
                                continue;
                            }

                            if (productIdCase3.Length != 3 || !productIdCase3.All(char.IsDigit))
                            {
                                ErrorMessage($"Ogiltigt produkt-ID. Ange ett 3-siffrigt numeriskt värde.");
                                continue;
                            }

                            string productName = GetUserInput("Ange produktens namn: ");

                            if (productName.Length > 20 || productName.Length < 1)
                            {
                                ErrorMessage($"Ogiltigt produkt namn. Ange något mellan 1-20 bokstäver.");
                                continue;
                            }

                            string priceTypeChoiceCase3 = GetUserInput("Styckpris eller kilopris (S/K)? ").ToLower();

                            if (priceTypeChoiceCase3 == "s" || priceTypeChoiceCase3 == "k")
                            {
                                Console.Write("Ange priset: ");
                                if (decimal.TryParse(Console.ReadLine().Trim(), out decimal newPriceCase3))
                                {
                                    if (newPriceCase3 > 0 && newPriceCase3 <= 50000)
                                    {
                                        productServices.AddProductWithPriceType(productIdCase3, productName, priceTypeChoiceCase3, newPriceCase3);
                                        BackToAdminMenuMsg();
                                        break;
                                    }
                                    else
                                    {
                                        ErrorMessage("Priset måste vara mellan 1 kr och 50 000 kr.");
                                    }
                                }
                                else
                                {
                                    ErrorMessage("Ogiltigt prisformat");
                                }
                            }

                            else
                            {
                                ErrorMessage($"Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
                            }
                        }
                        break;

                    case 4:
                        while (true)
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("╭────────────────────────────╮");
                            Console.WriteLine("│Ta bort produkt             │");
                            Console.WriteLine("│Q = tillbaka till adminmenyn│");
                            Console.WriteLine("╰────────────────────────────╯");
                            Console.ResetColor();

                            string productIdCase4 = GetUserInput("Ange 'Q' eller produkt ID: ").ToUpper();

                            if (productIdCase4 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase4))
                            {
                                ErrorMessage($"Produkt med ID {productIdCase4} finns ej.");
                                continue;
                            }

                            productServices.RemoveProduct(productIdCase4);

                            BackToAdminMenuMsg();
                            break;
                        }
                        break;

                    case 5:
                        while (true)
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("╭──────────────────────────────╮");
                            Console.WriteLine("│Lägga till rabatt             │");
                            Console.WriteLine("│1. Procentrabatt              │");
                            Console.WriteLine("│2. Mängdrabatt                │");
                            Console.WriteLine("│0. Tillbaka till adminmenyn   │");
                            Console.WriteLine("╰──────────────────────────────╯");
                            Console.ResetColor();

                            Console.Write("Ange ditt val: ");
                            int choice;

                            try
                            {
                                choice = Convert.ToInt32(Console.ReadLine().Trim());
                            }

                            catch (FormatException)
                            {
                                ErrorMessage($"Ogiltigt val. Ange en siffra.");
                                continue;
                            }

                            if (choice == 0)
                            {
                                break;
                            }
                            else if (choice == 1)
                            {
                                string productId = GetUserInput("Ange produkt ID: ");

                                if (!productServices.ProductExists(productId))
                                {
                                    ErrorMessage($"Produkt med ID {productId} finns ej.");
                                    continue;
                                }

                                Console.Write("Ange rabatt i procent (exempelvis 5 för 5% rabatt): ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal percentageDiscount) || percentageDiscount < 1 || percentageDiscount > 100)
                                {
                                    ErrorMessage($"Ogiltig rabatt, försök igen.\nAnge % rabatt mellan 1-99%");
                                    continue;
                                }

                                Console.Write("Ange startdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                                {
                                    ErrorMessage($"Ogiltigt startdatum. Ange datum i formatet 'yyyy-MM-dd'.");
                                    continue;
                                }

                                Console.Write("Ange slutdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate < DateTime.Today)
                                {
                                    ErrorMessage($"Ogiltigt slutdatum. Ange datum i formatet 'yyyy-MM-dd'.\nOBS kan ej sluta innan dagens datum");
                                    continue;
                                }

                                productServices.SetPercentageDiscount(productId, percentageDiscount, startDate, endDate);
                                BackToAdminMenuMsg();
                                break;
                            }
                            else if (choice == 2)
                            {
                                string productId = GetUserInput("Ange produkt ID: ");

                                if (!productServices.ProductExists(productId))
                                {
                                    ErrorMessage($"Produkt med ID {productId} finns ej.");
                                    continue;
                                }

                                Console.WriteLine("Exempel: ta 3 betala för 2, i det här fallet är X = 3");
                                Console.Write("Köp X antal: ");
                                if (!int.TryParse(Console.ReadLine(), out int buyQuantity) || buyQuantity <= 0)
                                {
                                    ErrorMessage($"Ogiltigt köp-antal, försök igen.");
                                    continue;
                                }

                                Console.WriteLine("Exempel: ta 3 betala för 2, i det här fallet är Y = 2");
                                Console.Write("Betala för Y: ");
                                if (!int.TryParse(Console.ReadLine(), out int payForQuantity) || payForQuantity <= 0)
                                {
                                    ErrorMessage($"Ogiltigt betal-antal, försök igen.");
                                    continue;
                                }

                                Console.Write("Ange startdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                                {
                                    ErrorMessage($"Ogiltigt startdatum. Ange datum i formatet 'yyyy-MM-dd'.");
                                    continue;
                                }

                                Console.Write("Ange slutdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate < DateTime.Today)
                                {
                                    ErrorMessage($"Ogiltigt slutdatum. Ange datum i formatet 'yyyy-MM-dd'.\nOBS kan ej sluta innan dagens datum");
                                    continue;
                                }

                                productServices.SetQuantityDiscount(productId, buyQuantity, payForQuantity, startDate, endDate);

                                BackToAdminMenuMsg();
                                break;
                            }
                        }
                        break;

                    case 6:
                        Console.Clear();
                        productServices.DisplayAvailableProducts();

                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine("╭────────────────────────────╮");
                        Console.WriteLine("│Ta bort kampanjpriser       │");
                        Console.WriteLine("│Q = tillbaka till adminmenyn│");
                        Console.WriteLine("╰────────────────────────────╯");
                        Console.ResetColor();

                        string productIdCase6 = GetUserInput("Ange 'Q' eller produkt ID: ").ToUpper();

                        if (productIdCase6 == "Q")
                        {
                            break;
                        }

                        productServices.RemoveDiscount(productIdCase6);

                        BackToAdminMenuMsg();
                        break;

                    case 0:
                        adminRunning = false;
                        Menu.MainMenu(productServices);
                        break;
                }
            } while (adminRunning);
        }
        /// <summary>
        /// Shows the admin menu
        /// </summary>
        private static void AdminMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("╭────────────────────────────╮");
            Console.WriteLine("│           Admin            │");
            Console.WriteLine("│ 1. Ändra namn på produkt   │");
            Console.WriteLine("│ 2. Ändra pris på produkt   │");
            Console.WriteLine("│ 3. Lägg till produkt       │");
            Console.WriteLine("│ 4. Ta bort produkt         │");
            Console.WriteLine("│ 5. Lägg till kampanjpris   │");
            Console.WriteLine("│ 6. Ta bort kampanjpris     │");
            Console.WriteLine("│ 0. Gå till huvudmenyn      │");
            Console.WriteLine("╰────────────────────────────╯");
            Console.ResetColor();
            Console.Write("Inmatning: ");
        }

        /// <summary>
        /// Shows error message if input is invalid
        /// </summary>
        /// <param name="message"></param>
        private static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.Write("Valfri knapp, försök igen");
            Console.ReadKey();
        }

        /// <summary>
        /// Gives message that user is going to go back to admin menu
        /// </summary>
        private static void BackToAdminMenuMsg()
        {
            Console.Write("Valfri knapp gå till adminmenyn");
            Console.ReadKey();
        }

        /// <summary>
        /// Gets the users input
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static string GetUserInput(string msg)
        {
            Console.Write(msg);
            string userInput = Console.ReadLine().Trim();
            return userInput;
        }
    }
}
