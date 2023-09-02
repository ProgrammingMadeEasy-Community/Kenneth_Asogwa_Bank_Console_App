using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class TransactionHistory
    {
        public void viewTransactionHistory(OpenAccount account)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Transaction History:");
            foreach (string transaction in account.TransactionHistory)
            {
                Console.WriteLine(transaction);
            }
        }
    }

}
