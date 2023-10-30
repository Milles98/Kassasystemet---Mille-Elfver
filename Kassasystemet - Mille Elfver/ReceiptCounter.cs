using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet___Mille_Elfver
{
    public class ReceiptCounter
    {
        private int _receiptCounter = 1337;

        /// <summary>
        /// Gets the current receipt number
        /// </summary>
        /// <returns></returns>
        public int GetReceiptNumber()
        {
            return _receiptCounter;
        }

        /// <summary>
        /// Increases the receipt counter by one
        /// </summary>
        public void IncreaseCounter()
        {
            _receiptCounter++;
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
                        _receiptCounter = counter;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel vid laddandet av kvittoräknare: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Saves the last receipt number to "KvittoRäknare" textfile and creates if it doesnt exist
        /// </summary>
        public void SaveReceiptCounter()
        {
            try
            {
                Directory.CreateDirectory($"../../../Kvitton");
                File.WriteAllText("../../../Kvitton/KvittoRäknare.txt", _receiptCounter.ToString());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Fel vid sparandet av kvittoräknare: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
