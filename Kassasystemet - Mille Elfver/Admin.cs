using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Admin
    {
        public static void AdminTools(ProductCatalog productCatalog)
        {
            AdminMenu();
            int userInput;
            if (!int.TryParse(Console.ReadLine(), out userInput))
            {
                Console.WriteLine("Ogiltig inmatning, försök igen.");
                return;
            }
            switch (userInput)
            {
                case 1:
                    Console.Clear();
                    productCatalog.DisplayAvailableProducts();
                    Console.WriteLine("\n1. Ändra namn på produkt");

                    Console.Write("Ange produkt-ID: ");
                    string productIdCase1 = Console.ReadLine();

                    Console.Write("Ange det nya namnet: ");
                    string newName = Console.ReadLine();

                    productCatalog.UpdateProductName(productIdCase1, newName);

                    Console.WriteLine("\nEnter för att gå till huvudmenyn");
                    break;

                case 2:
                    Console.Clear();
                    productCatalog.DisplayAvailableProducts();
                    decimal newUnitPrice;
                    decimal newKiloPrice;
                    Console.WriteLine("\n2. Ändra pris på produkter");

                    Console.Write("Ange produkt-ID: ");
                    string productIdCase2 = Console.ReadLine();

                    Console.Write("Ange nytt pris per styck: ");
                    decimal.TryParse(Console.ReadLine(), out newUnitPrice);

                    Console.Write("Ange nytt pris per kilo (0 om det är styckpris): ");
                    decimal.TryParse(Console.ReadLine(), out newKiloPrice);

                    productCatalog.UpdateProductPrice(productIdCase2, newUnitPrice, newKiloPrice);

                    Console.WriteLine("\nEnter för att gå till huvudmenyn");
                    break;

                case 3:
                    Console.Clear();
                    productCatalog.DisplayAvailableProducts();

                    Console.WriteLine("\n3. Lägg till ny produkt");

                    Console.Write("Ange produktens ID: ");
                    string productIdCase3 = Console.ReadLine();

                    Console.Write("Ange produktens namn: ");
                    string productName = Console.ReadLine();

                    Console.Write("Ange styckpris: ");

                    if (!decimal.TryParse(Console.ReadLine(), out decimal unitPrice))
                    {
                        Console.WriteLine("\nOgiltigt styckpris format");
                        break;
                    }
                    Console.Write("Ange kilopris (0 om det är styckpris): ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal kiloPrice))
                    {
                        Console.WriteLine("Ogiltigt kilopris format");
                        break;
                    }
                    productCatalog.AddProduct(productIdCase3, productName, unitPrice, kiloPrice);

                    Console.WriteLine("\nEnter för att gå till huvudmenyn");
                    break;

                case 4:
                    Console.Clear();
                    productCatalog.DisplayAvailableProducts();
                    Console.WriteLine("\n4. Ta bort produkt");

                    Console.Write("Ange produkt ID: ");
                    string productIdCase4 = Console.ReadLine();

                    productCatalog.RemoveProduct(productIdCase4);

                    Console.WriteLine("\nEnter för att gå till huvudmenyn");
                    break;

                case 5:
                    productCatalog.DisplayAvailableProducts();
                    Console.WriteLine("\n5. Lägga till kampanjpriser");
                    break;

                case 6:
                    productCatalog.DisplayAvailableProducts();
                    Console.WriteLine("\n6. Ta bort kampanjpriser");
                    break;

                case 0:
                    Menu.MainMenu(productCatalog);
                    break;
            }
        }
        public static void AdminMenu()
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
