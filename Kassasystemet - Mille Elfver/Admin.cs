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
                    Console.WriteLine("Ogiltig inmatning, valfri knapp försök igen.");
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
                            Console.WriteLine("\nÄndra namn på produkt");
                            Console.WriteLine("Q = tillbaka till adminmenyn\n");

                            Console.Write("Ange 'Q' eller produkt-ID: ");
                            string productIdCase1 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase1 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase1))
                            {
                                Console.WriteLine($"Produkt med ID {productIdCase1} finns ej.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

                            Console.Write("Ange det nya namnet: ");
                            string newName = Console.ReadLine().Trim();

                            productServices.UpdateProductName(productIdCase1, newName);

                            Console.Write("Valfri knapp gå till adminmenyn");
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

                            Console.WriteLine("\nÄndra pris på produkter");
                            Console.WriteLine("Q = tillbaka till adminmenyn\n");

                            Console.Write("Ange 'Q' eller produkt-ID: ");
                            string productIdCase2 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase2 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase2))
                            {
                                Console.WriteLine($"Produkt med ID {productIdCase2} finns ej.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
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
                                Console.WriteLine("Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

                            productServices.UpdateProductPrice(productIdCase2, newPrice, priceTypeChoice);

                            Console.Write("Enter för att gå till adminmenyn");
                            Console.ReadKey();
                            break;
                        }
                        break;

                    case 3:
                        while (true) 
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();

                            Console.WriteLine("\nLägg till ny produkt");
                            Console.WriteLine("Q = tillbaka till adminmenyn\n");

                            Console.Write("Ange 'Q' eller produktens ID: ");
                            string productIdCase3 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase3 == "Q")
                            {
                                break;
                            }

                            if (productServices.ProductExists(productIdCase3))
                            {
                                Console.WriteLine($"Produkt med ID {productIdCase3} finns redan.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

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
                                    Console.WriteLine("Ogiltigt pris format");
                                    Console.ReadKey();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ogiltig inmatning. Ange 'S' för styckpris eller 'K' för kilopris.");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case 4:
                        while (true)
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();
                            Console.WriteLine("\nTa bort produkt");
                            Console.WriteLine("Q = tillbaka till adminmenyn\n");

                            Console.Write("Ange 'Q' eller produkt ID: ");
                            string productIdCase4 = Console.ReadLine().Trim().ToUpper();

                            if (productIdCase4 == "Q")
                            {
                                break;
                            }

                            if (!productServices.ProductExists(productIdCase4))
                            {
                                Console.WriteLine($"Produkt med ID {productIdCase4} finns ej.");
                                Console.Write("Valfri knapp, försök igen.");
                                Console.ReadKey();
                                continue;
                            }

                            productServices.RemoveProduct(productIdCase4);

                            Console.Write("Enter för att gå till adminmenyn");
                            Console.ReadKey();
                            break;
                        }
                        break;

                    case 5:
                        while (true)
                        {
                            Console.Clear();
                            productServices.DisplayAvailableProducts();
                            Console.WriteLine("\nLägga till rabatt");
                            Console.WriteLine("1. Procentrabatt");
                            Console.WriteLine("2. Mängdrabatt");
                            Console.WriteLine("0. Tillbaka till adminmenyn\n");

                            Console.Write("Ange ditt val: ");
                            int choice = Convert.ToInt32(Console.ReadLine().Trim());

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
                                    Console.WriteLine($"Produkt med ID {productId} finns ej.");
                                    Console.Write("Valfri knapp, försök igen");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.Write("Ange rabatt i procent (exempelvis 5 för 5% rabatt): ");
                                if (!decimal.TryParse(Console.ReadLine(), out decimal percentageDiscount) || percentageDiscount <= 0)
                                {
                                    Console.WriteLine("Ogiltig rabatt, försök igen.");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.Write("Ange startdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                                {
                                    Console.WriteLine("Ogiltigt startdatum. Ange datum i formatet 'yyyy-MM-dd'.");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.Write("Ange slutdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                                {
                                    Console.WriteLine("Ogiltigt slutdatum. Ange datum i formatet 'yyyy-MM-dd'.");
                                    Console.ReadKey();
                                    continue;
                                }

                                productServices.SetPercentageDiscount(productId, percentageDiscount, startDate, endDate);
                                Console.Write("Valfri knapp, gå till adminmenyn");
                                Console.ReadKey();
                                break;
                            }
                            else if (choice == 2)
                            {
                                Console.Write("Ange produkt ID: ");
                                string productId = Console.ReadLine().Trim();

                                if (!productServices.ProductExists(productId))
                                {
                                    Console.WriteLine($"Produkt med ID {productId} finns ej.");
                                    Console.Write("Valfri knapp, försök igen");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.Write("Köp X antal: ");
                                if (!int.TryParse(Console.ReadLine(), out int buyQuantity) || buyQuantity <= 0)
                                {
                                    Console.WriteLine("Ogiltigt köp-antal, försök igen.");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.Write("Mängd som behöver uppnås för att få X antalet i rabatt: ");
                                if (!int.TryParse(Console.ReadLine(), out int payForQuantity) || payForQuantity <= 0)
                                {
                                    Console.WriteLine("Ogiltigt betal-antal, försök igen.");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.Write("Ange startdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                                {
                                    Console.WriteLine("Ogiltigt startdatum. Ange datum i formatet 'yyyy-MM-dd'.");
                                    Console.ReadKey();
                                    continue;
                                }

                                Console.Write("Ange slutdatum (yyyy-MM-dd): ");
                                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                                {
                                    Console.WriteLine("Ogiltigt slutdatum. Ange datum i formatet 'yyyy-MM-dd'.");
                                    Console.ReadKey();
                                    continue;
                                }

                                productServices.SetQuantityDiscount(productId, buyQuantity, payForQuantity, startDate, endDate);

                                Console.Write("Valfri knapp, gå till adminmenyn");
                                Console.ReadKey();
                                break;
                            }
                        }
                        break;

                    case 6:
                        Console.Clear();
                        productServices.DisplayAvailableProducts();
                        Console.WriteLine("\nTa bort kampanjpriser");
                        Console.WriteLine("Q = tillbaka till adminmenyn\n");

                        Console.Write("Ange 'Q' eller produkt ID: ");
                        string productIdCase6 = Console.ReadLine().Trim().ToUpper();

                        if (productIdCase6 == "Q")
                        {
                            break;
                        }

                        productServices.RemoveDiscount(productIdCase6);
                        Console.Write("Valfri knapp gå till adminmenyn");
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
            Console.Write("Inmatning: ");
        }
    }
}
