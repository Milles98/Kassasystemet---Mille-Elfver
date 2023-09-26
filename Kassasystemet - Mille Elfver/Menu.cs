using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class Menu
    {
        public static void MainMenu()
        {
            bool programRunning = true;
            do
            {
                //Menylista för kassasystemet 
                Console.Clear();
                Console.WriteLine("KASSA");
                Console.WriteLine("1. Ny kund");
                Console.WriteLine("0. Avsluta");
                Console.Write("Svar: ");

                int val;
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out val) && val >= 0 && val <= 1) //try parse istället för try catch
                {
                    switch (val)
                    {
                        //Kassan startas med ny försäljning
                        case 1:
                            Console.Clear();
                            Console.WriteLine("KASSA");
                            //om användaren väljer case 1 åker vi in i NewSale metoden!
                            string receipt = NewSale.NewSales();
                            break;

                        //Avslutar programmet
                        case 0:
                            Console.WriteLine("Tryck valfri knapp för att avsluta programmet.");
                            programRunning = false;
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ogiltig inmatning, tryck valfri knapp för att återgå till menyn och välj '1' eller '0'");
                }
                Console.ReadKey();

            } while (programRunning == true);
        }
    }


}
