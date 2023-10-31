using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Admin
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nOgiltig inmatning, valfri knapp försök igen.");
                    Console.ResetColor();
                    Console.ReadKey();
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

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Ange 'Q' eller produkt-ID: ");
                            string productIdCase1 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase1 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase1))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Produkt med ID {productIdCase1} finns ej.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Ange det nya namnet: ");
                            string newName = Console.ReadLine().Trim();

                            productServices.UpdateProductName(productIdCase1, newName);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Valfri knapp gå till adminmenyn");
                            Console.ResetColor();
                            Console.ReadKey();
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

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Ange 'Q' eller produkt-ID: ");
                            string productIdCase2 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase2 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase2))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Produkt med ID {productIdCase2} finns ej.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
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
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

                            productServices.UpdateProductPrice(productIdCase2, newPrice, priceTypeChoice);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Enter för att gå till adminmenyn");
                            Console.ResetColor();
                            Console.ReadKey();
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

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Ange 'Q' eller produktens ID: ");
                            string productIdCase3 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase3 == "Q")
                            {
                                break;
                            }

                            if (productServices.ProductExists(productIdCase3))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Produkt med ID {productIdCase3} finns redan.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

                            if (productIdCase3.Length != 3 || !productIdCase3.All(char.IsDigit))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Ogiltigt produkt-ID. Ange ett 3-siffrigt numeriskt värde.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Ange produktens namn: ");
                            string productName = Console.ReadLine().Trim();

                            Console.Write("Styckpris eller kilopris (S/K)? ");
                            string priceTypeChoiceCase3 = Console.ReadLine().Trim().ToLower();

                            if (priceTypeChoiceCase3 == "s" || priceTypeChoiceCase3 == "k")
                            {
                                Console.Write("Ange priset: ");
                                if (decimal.TryParse(Console.ReadLine().Trim(), out decimal newPriceCase3))
                                {
                                    productServices.AddProductWithPriceType(productIdCase3, productName, priceTypeChoiceCase3, newPriceCase3);
                                    Console.ReadKey();
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ogiltigt pris format");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
                                Console.ReadKey();
                            }
                        }
                        Console.ResetColor();
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

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Ange 'Q' eller produkt ID: ");
                            string productIdCase4 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase4 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase4))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Produkt med ID {productIdCase4} finns ej.");
                                Console.Write("Valfri knapp, försök igen.");
                                Console.ReadKey();
                                continue;
                            }

                            productServices.RemoveProduct(productIdCase4);

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Enter för att gå till adminmenyn");
                            Console.ResetColor();
                            Console.ReadKey();
                            break;
                        }
                        break;

                    case 5:
                        while (true)
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("\nLägga till rabatt");
                            Console.WriteLine("1. Procentrabatt");
                            Console.WriteLine("2. Mängdrabatt");
                            Console.WriteLine("0. Tillbaka till adminmenyn\n");

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Ange ditt val: ");
                            int choice;

                            try
                            {
                                choice = Convert.ToInt32(Console.ReadLine().Trim());
                            }

                            catch (FormatException)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Ogiltigt val. Ange en siffra.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
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
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Produkt med ID {productId} finns ej.");
                                    Console.Write("Valfri knapp, försök igen");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Ange rabatt i procent (exempelvis 5 för 5% rabatt): ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal percentageDiscount) || percentageDiscount <= 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ogiltig rabatt, försök igen.");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Ange startdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate) || startDate < DateTime.Today)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ogiltigt startdatum. Ange datum i formatet 'yyyy-MM-dd'.\nOBS kan ej börja innan dagens datum");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Ange slutdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate < DateTime.Today)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ogiltigt slutdatum. Ange datum i formatet 'yyyy-MM-dd'.\nOBS kan ej sluta innan dagens datum");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                productServices.SetPercentageDiscount(productId, percentageDiscount, startDate, endDate);
                                Console.Write("Valfri knapp, gå till adminmenyn");
                                Console.ResetColor();
                                Console.ReadKey();
                                break;
                            }
                            else if (choice == 2)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Ange produkt ID: ");
                                string productId = Console.ReadLine().Trim();

                                if (!productServices.ProductExists(productId))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine($"Produkt med ID {productId} finns ej.");
                                    Console.Write("Valfri knapp, försök igen");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Köp X antal: ");
                                if (!int.TryParse(Console.ReadLine(), out int buyQuantity) || buyQuantity <= 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ogiltigt köp-antal, försök igen.");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Mängd som behöver uppnås för att få X antalet i rabatt: ");
                                if (!int.TryParse(Console.ReadLine(), out int payForQuantity) || payForQuantity <= 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ogiltigt betal-antal, försök igen.");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Ange startdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate) || startDate < DateTime.Today)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ogiltigt startdatum. Ange datum i formatet 'yyyy-MM-dd'.\nOBS kan ej börja innan dagens datum");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Ange slutdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate) || endDate < DateTime.Today)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ogiltigt slutdatum. Ange datum i formatet 'yyyy-MM-dd'.\nOBS kan ej sluta innan dagens datum");
                                    Console.ReadKey();
                                    continue;
                                }

                                productServices.SetQuantityDiscount(productId, buyQuantity, payForQuantity, startDate, endDate);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Valfri knapp, gå till adminmenyn");
                                Console.ResetColor();
                                Console.ReadKey();
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

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Ange 'Q' eller produkt ID: ");
                        string productIdCase6 = Console.ReadLine().Trim().ToUpper();

                        if (productIdCase6 == "Q")
                        {
                            break;
                        }

                        productServices.RemoveDiscount(productIdCase6);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Valfri knapp gå till adminmenyn");
                        Console.ResetColor();
                        Console.ReadKey();
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Inmatning: ");
            Console.ResetColor();
        }
    }
}
