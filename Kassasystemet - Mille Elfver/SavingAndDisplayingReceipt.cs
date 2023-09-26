using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class SavingAndDisplayingReceipt
    {
        public static void SaveAndDisplayReceipt(string receipt)
        {

            //räknar ut totalen av allt som lagts in på kvittot och visar det
            decimal totalAmount = TotalAmountCalculator.CalculateTotalAmount(receipt);

            //får fram nuvarande tid till innehållet i kvittots textfil
            DateTime dateTime = DateTime.Now;

            //datum till kvittot när det skrivs ut (i textfilens rubrik) 
            var date = DateTime.Now.ToShortDateString();

            //formatterar datum och tid till en sträng
            string formattedDate = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            //denna gör så att 40 chars av - läggs på kvittot så att det blir lättare att skilja åt
            string receiptSeparator = new string('-', 40);

            //sätter maxlängd till 40 chars
            int maxWidth = 40;

            // räknar ut tillgängliga spacen för total: linjen
            int availableSpace = maxWidth - "Total:".Length;

            //lägger till total amount till höger och använder "C" för svensk currency
            string totalAmountText = totalAmount.ToString("C");

            //räknar ut paddingen för totala mängden till höger om kvittot
            string totalAmountPadding = new string(' ', availableSpace - totalAmountText.Length);

            //denna sträng skriver ut total: till vänster om kvittot
            string totalLine = $"Total:{totalAmountPadding}{totalAmountText}";

            //lägger till datumet och total summan på kvittot:
            string receiptWithDateandTotalAmount = $"\n{receiptSeparator}\nKVITTO {formattedDate.PadLeft(33)}\n\n{receipt}\n{totalLine}\n{receiptSeparator}";

            //filnamn för kvittot med kvittonummer och datum
            string fileName = $"Kvitto - {date}.txt";

            //sparar ned kvittot med en Append så att det fylls på med nya kvitton
            File.AppendAllText($"../../../Receipts/{fileName}", receiptWithDateandTotalAmount);

            Console.WriteLine(receiptWithDateandTotalAmount);
        }
    }
}
