using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ReceiptCounter
    {
        private int receiptCounter = 1337;

        public int GetReceiptNumber()
        {
            return receiptCounter;
        }
        public void IncreaseCounter()
        {
            receiptCounter++;
            SaveReceiptCounter(); 
        }
        /// <summary>
        /// Loads the latest receipt number
        /// </summary>
        public void LoadReceiptCounter()
        {
            try
            {
                if (File.Exists("../../../Kvitton/KvittoRäknare.txt"))
                {
                    string counterText = File.ReadAllText("../../../Kvitton/KvittoRäknare.txt");
                    if (int.TryParse(counterText, out int counter))
                    {
                        receiptCounter = counter;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid laddandet av kvittoräknare: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves the last receipt number to "KvittoRäknare" textfile
        /// </summary>
        public void SaveReceiptCounter()
        {
            try
            {
                Directory.CreateDirectory($"../../../Kvitton");
                File.WriteAllText("../../../Kvitton/KvittoRäknare.txt", receiptCounter.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparandet av kvittoräknare: {ex.Message}");
            }
        }
    }
}
