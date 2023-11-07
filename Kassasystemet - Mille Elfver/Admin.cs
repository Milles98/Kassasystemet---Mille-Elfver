using System;
using System.Collections.Generic;
using System.Linq;
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
                            Console.WriteLine("\n╭────────────────────────────╮");
                            Console.WriteLine("│Ändra namn på produkt       │");
                            Console.WriteLine("│Q = tillbaka till adminmenyn│");
                            Console.WriteLine("╰────────────────────────────╯");
                            Console.ResetColor();

                            Console.Write("Ange 'Q' eller produkt-ID: ");
                            string productIdCase1 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase1 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase1))
                            {
                                ErrorMessage($"Produkt med ID {productIdCase1} finns ej.");
                                continue;
                            }

                            Console.Write("Ange det nya namnet: ");
                            string newName = Console.ReadLine().Trim();
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
                            decimal newPrice;

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("\n╭────────────────────────────╮");
                            Console.WriteLine("│Ändra pris på produkter     │");
                            Console.WriteLine("│Q = tillbaka till adminmenyn│");
                            Console.WriteLine("╰────────────────────────────╯");
                            Console.ResetColor();

                            Console.Write("Ange 'Q' eller produkt-ID: ");
                            string productIdCase2 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase2 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase2))
                            {
                                ErrorMessage($"Produkt med ID {productIdCase2} finns ej.");
                                continue;
                            }

                            Console.Write("Vill du ändra till styckpris eller kilopris (S/K)? ");
                            string priceTypeChoice = Console.ReadLine().Trim().ToLower();

                            if (priceTypeChoice == "s")
                            {
                                Console.Write("Ange nytt pris per styck: ");
                                decimal.TryParse(Console.ReadLine().Trim(), out newPrice);
                            }
                            else if (priceTypeChoice == "k")
                            {
                                Console.Write("Ange nytt pris per kilo: ");
                                decimal.TryParse(Console.ReadLine().Trim(), out newPrice);
                            }
                            else
                            {
                                ErrorMessage($"Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
                                continue;
                            }

                            productServices.UpdateProductPrice(productIdCase2, newPrice, priceTypeChoice);

                            BackToAdminMenuMsg();
                            break;
                        }
                        break;

                    case 3:
                        while (true)
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("\n╭────────────────────────────╮");
                            Console.WriteLine("│Lägg till ny produkt        │");
                            Console.WriteLine("│Q = tillbaka till adminmenyn│");
                            Console.WriteLine("╰────────────────────────────╯");
                            Console.ResetColor();

                            Console.Write("Ange 'Q' eller produktens ID: ");
                            string productIdCase3 = Console.ReadLine().Trim().ToUpper();

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

                            Console.Write("Ange produktens namn: ");
                            string productName = Console.ReadLine().Trim();
                            if (productName.Length > 20 || productName.Length < 1)
                            {
                                ErrorMessage($"Ogiltigt produkt namn. Ange något mellan 1-20 bokstäver.");
                                continue;
                            }

                            Console.Write("Styckpris eller kilopris (S/K)? ");
                            string priceTypeChoiceCase3 = Console.ReadLine().Trim().ToLower();

                            if (priceTypeChoiceCase3 == "s" || priceTypeChoiceCase3 == "k")
                            {
                                Console.Write("Ange priset: ");
                                if (decimal.TryParse(Console.ReadLine().Trim(), out decimal newPriceCase3))
                                {
                                    productServices.AddProductWithPriceType(productIdCase3, productName, priceTypeChoiceCase3, newPriceCase3);
                                    BackToAdminMenuMsg();
                                    break;
                                }
                                else
                                {
                                    ErrorMessage($"Ogiltigt pris format");
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
                            Console.WriteLine("\n╭────────────────────────────╮");
                            Console.WriteLine("│Ta bort produkt             │");
                            Console.WriteLine("│Q = tillbaka till adminmenyn│");
                            Console.WriteLine("╰────────────────────────────╯");
                            Console.ResetColor();

                            Console.Write("Ange 'Q' eller produkt ID: ");
                            string productIdCase4 = Console.ReadLine().Trim().ToUpper();

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
                            Console.WriteLine("\n╭──────────────────────────────╮");
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
                                Console.Write("Ange produkt ID: ");
                                string productId = Console.ReadLine().Trim();

                                if (!productServices.ProductExists(productId))
                                {
                                    ErrorMessage($"Produkt med ID {productId} finns ej.");
                                    continue;
                                }

                                Console.Write("Ange rabatt i procent (exempelvis 5 för 5% rabatt): ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal percentageDiscount) || percentageDiscount < 1 || percentageDiscount >= 100)
                                {
                                    ErrorMessage($"Ogiltig rabatt, försök igen.\nAnge % rabatt mellan 1-100%");
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
                                Console.Write("Ange produkt ID: ");
                                string productId = Console.ReadLine().Trim();

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
                        Console.WriteLine("\n╭────────────────────────────╮");
                        Console.WriteLine("│Ta bort kampanjpriser       │");
                        Console.WriteLine("│Q = tillbaka till adminmenyn│");
                        Console.WriteLine("╰────────────────────────────╯");
                        Console.ResetColor();

                        Console.Write("Ange 'Q' eller produkt ID: ");
                        string productIdCase6 = Console.ReadLine().Trim().ToUpper();

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

        private static void ErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.Write("Valfri knapp, försök igen");
            Console.ReadKey();
        }

        private static void BackToAdminMenuMsg()
        {
            Console.Write("Valfri knapp gå till adminmenyn");
            Console.ReadKey();
        }
    }
}
