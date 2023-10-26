using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Admin
    {
        public static void AdminTools(ProductServices productCatalog)
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
                            productCatalog.DisplayAvailableProducts();
                            Console.WriteLine("\n1. Ändra namn på produkt");

                            Console.Write("Ange produkt-ID: ");
                            string productIdCase1 = Console.ReadLine().Trim();

                            if (!productCatalog.ProductExists(productIdCase1))
                            {
                                Console.WriteLine($"Produkt med ID {productIdCase1} finns ej.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

                            Console.Write("Ange det nya namnet: ");
                            string newName = Console.ReadLine().Trim();

                            productCatalog.UpdateProductName(productIdCase1, newName);

                            Console.Write("Valfri knapp gå till adminmenyn");
                            Console.ReadKey();
                            break;
                        }
                        break;

                    case 2:
                        while (true)
                        {
                            Console.Clear();
                            productCatalog.DisplayAvailableProducts();
                            decimal newPrice;

                            Console.WriteLine("\n2. Ändra pris på produkter");

                            Console.Write("Ange produkt-ID: ");
                            string productIdCase2 = Console.ReadLine().Trim();

                            if (!productCatalog.ProductExists(productIdCase2))
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

                            productCatalog.UpdateProductPrice(productIdCase2, newPrice, priceTypeChoice);

                            Console.Write("Enter för att gå till adminmenyn");
                            Console.ReadKey();
                            break;
                        }
                        break;

                    case 3:
                        while (true) 
                        {
                            Console.Clear();
                            productCatalog.DisplayAvailableProducts();

                            Console.WriteLine("\n3. Lägg till ny produkt");

                            Console.Write("Ange produktens ID: ");
                            string productIdCase3 = Console.ReadLine().Trim();

                            if (productCatalog.ProductExists(productIdCase3))
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
                                    productCatalog.AddProductWithPriceType(productIdCase3, productName, priceTypeChoiceCase3, newPriceCase3);
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
                            productCatalog.DisplayAvailableProducts();
                            Console.WriteLine("\n4. Ta bort produkt");

                            Console.Write("Ange produkt ID: ");
                            string productIdCase4 = Console.ReadLine().Trim();

                            if (!productCatalog.ProductExists(productIdCase4))
                            {
                                Console.WriteLine($"Produkt med ID {productIdCase4} finns ej.");
                                Console.Write("Valfri knapp, försök igen.");
                                Console.ReadKey();
                                continue;
                            }

                            productCatalog.RemoveProduct(productIdCase4);

                            Console.Write("Enter för att gå till adminmenyn");
                            Console.ReadKey();
                            break;
                        }
                        break;

                    case 5:
                        while (true)
                        {
                            Console.Clear();
                            productCatalog.DisplayAvailableProducts();
                            Console.WriteLine("\n5. Lägga till kampanjpriser");

                            Console.Write("Ange produkt ID: ");
                            string productIdCase5 = Console.ReadLine().Trim();

                            if (!productCatalog.ProductExists(productIdCase5))
                            {
                                Console.WriteLine($"Produkt med ID {productIdCase5} finns ej.");
                                Console.Write("Valfri knapp, försök igen");
                                Console.ReadKey();
                                continue;
                            }

                            Console.Write("Ange rabatt (exempelvis 5 blir 5% rabatt etc): ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal productIdCase5Discount) || productIdCase5Discount <= 0)
                            {
                                Console.WriteLine("Ogiltig rabatt, försök igen.");
                                Console.ReadKey();
                                continue;
                            }

                            Console.Write("Ange startdatum (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime productIdCase5StartDate))
                            {
                                Console.WriteLine("Ogiltigt startdatum. Ange datum i formatet 'yyyy-mm-dd'.");
                                Console.ReadKey();
                                continue;
                            }

                            Console.Write("Ange slutdatum (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime productIdCase5EndDate))
                            {
                                Console.WriteLine("Ogiltigt slutdatum. Ange datum i formatet 'yyyy-mm-dd'.");
                                Console.ReadKey();
                                continue;
                            }

                            productCatalog.SetDiscount(productIdCase5, productIdCase5Discount, productIdCase5StartDate, productIdCase5EndDate);
                            Console.Write("Valfri knapp gå till adminmenyn");
                            Console.ReadKey();
                            break;
                        }
                        break;

                    case 6:
                        Console.Clear();
                        productCatalog.DisplayAvailableProducts();
                        Console.WriteLine("\n6. Ta bort kampanjpriser");

                        Console.Write("Ange produkt ID: ");
                        string productIdCase6 = Console.ReadLine().Trim();

                        productCatalog.RemoveDiscount(productIdCase6);
                        Console.Write("Valfri knapp gå till adminmenyn");
                        Console.ReadKey();
                        break;

                    case 0:
                        adminRunning = false;
                        Menu.MainMenu(productCatalog);
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
