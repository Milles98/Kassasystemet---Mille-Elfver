using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class SavingAndDisplayingReceipt
    {
        /// <summary>
        /// Saves all product input from user, shows receipt and saves it to .txt file
        /// </summary>
        /// <param name="receipt"></param>
        public static void SaveAndDisplayReceipt(string receipt)
        {
            decimal totalAmount = TotalAmountCalculator.CalculateTotalAmount(receipt); //räknar ut totalen av allt som lagts in på kvittot och visar det

            DateTime dateTime = DateTime.Now; //får fram nuvarande tid till innehållet i kvittots textfil
            string formattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss"); //formatterar datum och tid till en sträng

            string receiptSeparator = new string('-', 40); //denna gör så att 40 chars av - läggs på kvittot så att det blir lättare att skilja åt
            int maxWidth = 40; //sätter maxlängd till 40 chars
            int availableSpace = maxWidth - "Total:".Length; // räknar ut tillgängliga spacen för total: linjen
            string totalAmountText = totalAmount.ToString("C"); //lägger till total amount till höger och använder "C" för svensk currency
            string totalAmountPadding = new string(' ', availableSpace - totalAmountText.Length); //räknar ut paddingen för totala mängden till höger om kvittot
            string totalLine = $"Total:{totalAmountPadding}{totalAmountText}"; //denna sträng skriver ut total: till vänster om kvittot

            //lägger till datumet och total summan på kvittot:
            string receiptWithDateandTotalAmount = $"\n{receiptSeparator}\nKVITTO {formattedDate.PadLeft(33)}\n\n{receipt}\n{totalLine}\n{receiptSeparator}";


            var date = DateTime.Now.ToShortDateString(); //datum till kvittot när det skrivs ut (i textfilens rubrik) 
            string fileName = $"Kvitto - {date}.txt"; //filnamn för kvittot med kvittonummer och datum

            //sparar ned kvittot med en Append så att det fylls på med nya kvitton
            File.AppendAllText($"../../../Receipts/{fileName}", receiptWithDateandTotalAmount);

            Console.WriteLine(receiptWithDateandTotalAmount);


        }

    }
}
