﻿using System.Collections.Generic;

namespace Kassasystemet___Mille_Elfver
{
    internal class Program
    {

        //Programmera ett kassasystem - (som man har i kassan i en matbutik)
        //använd double till talen (avrunar typ banan till 19.50 (2 decimaler) kan använda Math.Round med 2 decimaler
        //man får dock använda int istället om det blir jobbigt
        //Kan göra kvitto tabell med ,30 osv, kolla övningsuppgifter

        // ToUpper() (att använda till att göra "PAY" till stora bokstäver) nog bra att lösa med string kanske? osäker
        //Console.WriteLine("Skriv in ditt namn");
        //string name = Console.ReadLine();
        //char firstLetter = name[0];
        //string newName = char.ToUpper(firstLetter) + name.Substring(1).ToLower();
        //Console.WriteLine(newName);

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
            Menu.MainMenu();
        }

    }

}
