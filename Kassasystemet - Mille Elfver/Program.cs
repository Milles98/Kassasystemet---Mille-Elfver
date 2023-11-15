using System.Collections.Generic;
using Autofac;

namespace Kassasystemet___Mille_Elfver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Console.WriteLine("Förord");
            //Console.WriteLine("1. Öppna konsolen i helskärm för bättre användarupplevelse.");
            //Console.WriteLine("2. Jag har haft problem med windows konsolen/terminalen.");
            //Console.WriteLine("Om det ser buggigt ut ändra terminalinställning från windows terminal till windows konsolvärd.");
            //Console.WriteLine("Tryck valfri knapp för att fortsätta.");
            //Console.ReadKey();

            using (var container = AutoFacRegistration.RegisteredContainers())
            {
                var productServices = container.Resolve<IProductServices>();
                MainMenu.Menu(productServices);
            }

            //ProductServices productServices = new ProductServices();

            //MainMenu.Menu(productServices);
        }

    }

}
