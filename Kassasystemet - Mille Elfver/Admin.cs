using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Admin
    {
        public static void AdminTools()
        {
            AdminMenu();
            int userInput = Convert.ToInt32(Console.ReadLine());
            switch (userInput)
            {
                case 1:
                    ProductCatalog productCatalog = new ProductCatalog();

                    //Console.Write("Ange namn på produkt: ");
                    //string userAnswerId = Console.ReadLine();

                    //Console.Write("Ange pris på produkt: ");
                    //var productAnswerId = Console.ReadLine();

                    //productCatalog.availableProducts.Add(userAnswerId, productAnswerId);

                    break;
                case 2:
                    Console.WriteLine("2. Ändra pris på produkter");
                    break;
                case 3:
                    Console.WriteLine("3. Lägga till produkter");
                    break;
                case 4:
                    Console.WriteLine("4. Lägga till/ta bort kampanjpriser");
                    break;
                case 5:
                    Menu.MainMenu();
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
