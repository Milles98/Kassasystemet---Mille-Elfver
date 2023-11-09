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
        FileManager fileManager = new FileManager();
        public void SetReceiptCounter(int receiptCounter)
        {
            if (receiptCounter > 0)
            {
                _receiptCounter = receiptCounter;
            }
            else
            {
                _receiptCounter = 1337;
            }
        }
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
            fileManager.SaveReceiptCounter(_receiptCounter);
        }
    }
}
