using System.Collections.Generic;

namespace Kassasystemet___Mille_Elfver
{
    internal class Program
    {
        //Kassasystemet how to:
        //0. Data seeding en fil med produkter
        //allt i en stor loop
        //1. Göra meny (cw)
        //2. User input i en variabel, kan använda (switch) (console.readline) (if sats) (loopar)
        //3. Göra string manipulation (split) och spara i en (array[300] är kod), (array[300, 2] andra är antal
        //4. file IO 
        //5. PAY
        //6. file IO igen
        //7. ett meddelande (vad användaren gjort) sedan går tbx till 1 eller avslutar
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //Console.WriteLine("Förord");
            //Console.WriteLine("1. Öppna konsolen i helskärm för bättre användarupplevelse.");
            //Console.WriteLine("2. Jag har haft problem med windows konsolen/terminalen,\nom det ser buggigt ut ändra terminalinställning från windows terminal till windows konsolvärd ");
            //Console.WriteLine("Tryck valfri knapp för att fortsätta.");
            //Console.ReadKey();

            ProductCatalog productCatalog = new ProductCatalog();

            Menu.MainMenu(productCatalog);
        }

    }

}
