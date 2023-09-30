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
            bool programRunning = true;
            while (programRunning)
            {
                AdminMenu();
                int userInput = Convert.ToInt32(Console.ReadLine());

                switch (userInput)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        Menu.MainMenu();
                        break;
                }
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
        }

    }
}
