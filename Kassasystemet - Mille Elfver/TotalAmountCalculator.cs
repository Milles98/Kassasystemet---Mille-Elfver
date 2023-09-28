using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class TotalAmountCalculator
    {
        /// <summary>
        /// Calculates all products added from user, saves it to totalAmount string that is then returned
        /// </summary>
        /// <param name="receipt"></param>
        /// <returns></returns>
        public decimal CalculateTotalAmount(string receipt)
        {
            //splittar produkterna i kvittot till individuella rader
            string[] linesInReceipt = receipt.Split('\n');

            //tilldelar variabeln totalAmount 0, så fylls den på nedan
            decimal totalAmount = 0m;

            //foreach loop som kollar inuti varje rad 
            foreach (string line in linesInReceipt)
            {
                //splittar innehållet i varje rad till enskilda delar
                string[] partsInReceipt = line.Split(' ');

                //om raden är större än eller lika med 4, plussa på priset på totalen
                if (partsInReceipt.Length >= 4)
                {
                    //hämtar totalprice från metoden AddProductsToReceipt sedan plussar på det i totalAmount
                    if (decimal.TryParse(partsInReceipt[partsInReceipt.Length - 1], out decimal totalPrice))
                    {
                        totalAmount += totalPrice;
                    }

                }
            }
            return totalAmount;
        }
    }
}
