using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class FundDeposit
    {
        public void makeDeposit(OpenAccount accountToDeposit, double depositAmount)
        {
            if (accountToDeposit == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            if (depositAmount <= 0)
            {
                Console.WriteLine("Invalid deposit amount.");
                return;
            }
            accountToDeposit.Balance += depositAmount;
            accountToDeposit.TransactionHistory.Add($"Amount Credited: {depositAmount}");
            accountToDeposit.TransactionHistory.Add($"Credited By: {accountToDeposit.Fullname}");
            accountToDeposit.TransactionHistory.Add($"Balance: {accountToDeposit.Balance}");
        }
    }
}
