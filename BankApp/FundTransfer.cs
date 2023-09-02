using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class FundTransfer
    {
        FundWithdrawal withdrawal = new FundWithdrawal();
        FundDeposit deposit = new FundDeposit();

        public bool makeTransfer(OpenAccount senderAccount, OpenAccount recipientAccount, double amount, int pin)
        {
            if (pin != senderAccount.Pin)
            {
                return false;
            }


            if (withdrawal.makeWithdrawal(senderAccount, amount))
            {
                // Deduct the amount from the sender's account
                senderAccount.Balance -= amount;
                //senderAccount.TransactionHistory.Add($"Amount Transfered: {amount}");
                //senderAccount.TransactionHistory.Add($"Transfered By: {senderAccount.Fullname}");
                //senderAccount.TransactionHistory.Add($"Balance: {senderAccount.Balance}");

                // Add the amount to the recipient's account
                recipientAccount.Balance += amount;
                recipientAccount.TransactionHistory.Add($"Amount Credited: {amount}");
                recipientAccount.TransactionHistory.Add($"Credited By: {senderAccount.Fullname}");
                recipientAccount.TransactionHistory.Add($"Balance: {senderAccount.Balance}");
                return true;    
            }
            return true;

        }
    }
}
