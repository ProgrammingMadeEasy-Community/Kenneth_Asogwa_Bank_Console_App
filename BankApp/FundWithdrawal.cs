using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class FundWithdrawal
    {
        public bool makeWithdrawal(OpenAccount accountToWithdraw, double withdrawAmount)
        {
            if (withdrawAmount <= 0 || withdrawAmount > accountToWithdraw.Balance)
            {
                return false;
            }

            accountToWithdraw.Balance -= withdrawAmount;
            accountToWithdraw.TransactionHistory.Add($"Amount Withdrawn: {withdrawAmount}");
            accountToWithdraw.TransactionHistory.Add($"Withdrawn By: {accountToWithdraw.Fullname}");
            accountToWithdraw.TransactionHistory.Add($"Balance: {accountToWithdraw.Balance}");
            return true;
        }
    }
}
