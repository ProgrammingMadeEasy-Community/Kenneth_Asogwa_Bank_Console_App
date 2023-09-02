using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class ViewAccountDetails
    {
        public void viewAccountDetails(OpenAccount account)
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine("Account Details:");
            Console.WriteLine($"Account Holder: {account.Fullname}");
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Email: {account.Email}");
            Console.WriteLine($"Phone Number: {account.PhoneNumber}");
            Console.WriteLine($"Current Balance: {account.Balance}");
        }
    }
}
