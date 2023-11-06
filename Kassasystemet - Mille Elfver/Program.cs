using System.Collections.Generic;

namespace Kassasystemet___Mille_Elfver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Console.WriteLine("Förord");
            //Console.WriteLine("1. Öppna konsolen i helskärm för bättre användarupplevelse.");
            //Console.WriteLine("2. Jag har haft problem med windows konsolen/terminalen,\nOm det ser buggigt ut ändra terminalinställning från windows terminal till windows konsolvärd ");
            //Console.WriteLine("Tryck valfri knapp för att fortsätta.");
            //Console.ReadKey();

            ProductServices productServices = new ProductServices();

            Menu.MainMenu(productServices);
        }

    }

}
