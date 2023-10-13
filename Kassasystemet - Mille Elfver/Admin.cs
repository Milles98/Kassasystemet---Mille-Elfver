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
            //ProductCatalog productCatalog = new ProductCatalog();
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    Console.WriteLine("Ändra namn på produkt");

                    Console.Write("Ange produkt-ID: ");
                    string productIdCase1 = Console.ReadLine();

                    Console.Write("Ange det nya namnet: ");
                    string newName = Console.ReadLine();

                    productCatalog.UpdateProductName(productIdCase1, newName);
                    break;

                case 2:
                    Console.WriteLine("2. Ändra pris på produkter");

                    Console.Write("Ange produkt-ID: ");
                    string productIdCase2 = Console.ReadLine();

                    Console.Write("Ange nytt pris per styck: ");
                    decimal newUnitPrice = decimal.Parse(Console.ReadLine());

                    Console.Write("Ange nytt pris per kilo(0 om det är styckpris): ");
                    decimal newKiloPrice = decimal.Parse(Console.ReadLine());

                    productCatalog.UpdateProductPrice(productIdCase2, newUnitPrice, newKiloPrice);
                    break;

                case 3:
                    Console.WriteLine("3. Lägga till produkter\n");
                    Console.Write("Ange produktens ID: ");
                    string productIdCase3 = Console.ReadLine();
                    Console.Write("Ange produktens namn: ");
                    string productName = Console.ReadLine();
                    Console.Write("Ange styckpris: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal unitPrice))
                    {
                        Console.WriteLine("Ogiltigt styckpris format");
                        break;
                    }
                    Console.Write("Ange kilopris (0 om det är styckpris): ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal kiloPrice))
                    {
                        Console.WriteLine("Ogiltigt kilopris format");
                        break;
                    }
                    productCatalog.AddProduct(productIdCase3, productName, unitPrice, kiloPrice);
                    break;

                case 4:
                    Console.WriteLine("4. Lägga till/ta bort kampanjpriser");
                    break;

                case 5:
                    Menu.MainMenu(productCatalog);
                    break;
            }
        }
        public static void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("Admin");
            Console.WriteLine("1. Ändra namn på produkter");
            Console.WriteLine("2. Ändra pris på produkter");
            Console.WriteLine("3. Lägga till produkter");
            Console.WriteLine("4. Lägga till/ta bort kampanjpriser");
            Console.WriteLine("5. Gå till huvudmenyn");
            Console.Write("Inmatning: ");
        }

    }
}
