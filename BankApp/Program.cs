using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    class BankApplication
    {
        static OpenAccount CreateAccount()
        {
            OpenAccount newAccount = new OpenAccount();
            Console.WriteLine("-------------------------");
            Console.Write("Enter Your Fullname: ");
            newAccount.Fullname = Console.ReadLine();

            Console.Write("Enter Email: ");
            newAccount.Email = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            newAccount.PhoneNumber = Console.ReadLine();

            // Perform the valifation before returning the account object
            var validationContext = new ValidationContext(newAccount, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(newAccount, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var validationResult in validationResults)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
                return null;
            }
            return newAccount;
        }

        static void Main(string[] args)
        {
            List<OpenAccount> accounts = new List<OpenAccount>();
            FundDeposit fundDeposit = new FundDeposit();   
            FundWithdrawal fundWithdrawal = new FundWithdrawal();
            FundTransfer fundTransfer = new FundTransfer();
            ViewAccountDetails details = new ViewAccountDetails();
            TransactionHistory transactionHistory = new TransactionHistory();

            while (true)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Services");
                Console.WriteLine("1. Open Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdrawal");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. View Account Details/Balance");
                Console.WriteLine("6. View Transaction History");
                Console.WriteLine("7. Exit");
                Console.Write("Choose service: ");

                if (int.TryParse(Console.ReadLine(), out int operation))
                {
                    switch (operation)
                    {
                        case 1:
                            OpenAccount newAccount = CreateAccount();
                            if (newAccount != null)
                            {
                                accounts.Add(newAccount);
                                Console.WriteLine("Account created successfully.");
                                Console.WriteLine("-------------------------");
                                Console.WriteLine($"Account Number: {newAccount.AccountNumber}");
                                Console.WriteLine($"Account Pin: {newAccount.Pin}");

                            }
                            break;
                        case 2:
                            if (accounts.Count == 0)
                            {
                                Console.WriteLine("No accounts available. Please create an account first.");
                            }
                            else
                            {
                                Console.Write("Enter Account Number: ");
                                if (double.TryParse(Console.ReadLine(), out double accountNumber))
                                {
                                    OpenAccount accountToDeposit = accounts.Find(a => a.AccountNumber == accountNumber);
                                    if (accountToDeposit != null)
                                    {
                                        // Print the associated account name
                                        Console.WriteLine($"Account Name: {accountToDeposit.Fullname}");

                                        Console.Write("Enter Deposit Amount: ");
                                        if (double.TryParse(Console.ReadLine(), out double depositAmount))
                                        {
                                            // Use the FundDeposit object to make the deposit
                                            fundDeposit.makeDeposit(accountToDeposit, depositAmount);
                                            Console.WriteLine("-------------------------");
                                            Console.WriteLine($"You have successfully deposited: {depositAmount}");
                                            
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid deposit amount.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Account not found.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid account number input.");
                                }
                            }
                            break;
                        case 3:
                            if (accounts.Count == 0)
                            {
                                Console.WriteLine("No accounts available. Please create an account first.");
                            }
                            else
                            {
                                Console.Write("Enter Account Number: ");
                                if (double.TryParse(Console.ReadLine(), out double accountNumber))
                                {
                                    OpenAccount accountToWithdraw = accounts.Find(a => a.AccountNumber == accountNumber);
                                    if (accountToWithdraw != null)
                                    {
                                        int incorrectPinAttempts = 0; // Initialize the count of incorrect PIN attempts.

                                        while (incorrectPinAttempts < 3) // Allow up to 3 incorrect attempts.
                                        {
                                            Console.Write("Enter your 4-digit PIN: ");
                                            if (int.TryParse(Console.ReadLine(), out int enteredPIN))
                                            {
                                                if (enteredPIN == accountToWithdraw.Pin)
                                                {
                                                    Console.Write("Enter Withdrawal Amount: ");
                                                    if (double.TryParse(Console.ReadLine(), out double withdrawAmount))
                                                    {
                                                        // Use the FundWithdrawal object to make the withdrawal
                                                        if (fundWithdrawal.makeWithdrawal(accountToWithdraw, withdrawAmount))
                                                        {
                                                            Console.WriteLine("-------------------------");
                                                            Console.WriteLine($"You have successfully withdrawn: {withdrawAmount}");
                                                            //Console.WriteLine($"Account balance: {accountToWithdraw.Balance}");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Withdrawal failed. Insufficient balance or invalid amount.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid withdrawal amount.");
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid PIN. Access denied.");
                                                    incorrectPinAttempts++; // Increment the incorrect attempts count.
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid PIN format.");
                                            }
                                        }

                                        if (incorrectPinAttempts >= 3)
                                        {
                                            Console.WriteLine("Account locked due to too many incorrect PIN attempts.");
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Account not found.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid account number input.");
                                }
                            }
                            break;

                        case 4:
                            if (accounts.Count == 0)
                            {
                                Console.WriteLine("No accounts available. Please create an account first.");
                            }
                            else
                            {
                                Console.Write("Enter Sender Account Number: ");
                                if (double.TryParse(Console.ReadLine(), out double senderAccountNumber))
                                {
                                    OpenAccount senderAccount = accounts.Find(a => a.AccountNumber == senderAccountNumber);
                                    if (senderAccount != null)
                                    {
                                        int incorrectPinAttempts = 0;
                                        while(incorrectPinAttempts < 3)
                                        {
                                            Console.Write("Enter Sender 4-digit PIN: ");
                                            if (int.TryParse(Console.ReadLine(), out int senderPIN))
                                            {
                                                if (senderPIN == senderAccount.Pin)
                                                {
                                                    Console.Write("Enter Recipient's Account Number: ");
                                                    if (double.TryParse(Console.ReadLine(), out double recipientAccountNumber))
                                                    {
                                                        OpenAccount recipientAccount = accounts.Find(a => a.AccountNumber == recipientAccountNumber);
                                                        if (recipientAccount != null)
                                                        {
                                                            // Print the associated account name
                                                            Console.WriteLine($"Recipient Account Name: {recipientAccount.Fullname}");

                                                            Console.Write("Enter Transfer Amount: ");
                                                            if (double.TryParse(Console.ReadLine(), out double transferAmount))
                                                            {
                                                                // Use the FundTransfer object to make the transfer
                                                                if (fundTransfer.makeTransfer(senderAccount, recipientAccount, transferAmount, senderPIN))
                                                                {
                                                                    Console.WriteLine("-------------------------");
                                                                    Console.WriteLine($"You have successfully transferred {transferAmount} to Account Number: {recipientAccount.AccountNumber}");

                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine("Transfer failed. Invalid PIN or insufficient balance.");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Invalid transfer amount.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine("Recipient's account not found.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Invalid recipient's account number input.");
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid PIN. Access denied.");
                                                    incorrectPinAttempts++;
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid sender's PIN format.");
                                            }

                                        }
                                        if (incorrectPinAttempts >= 3)
                                        {
                                            Console.WriteLine("Account locked due to too many incorrect PIN attempts.");
                                            Environment.Exit(0);
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Sender's account not found.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid sender's account number input.");
                                }
                            }
                            break;
                        case 5:
                            if (accounts.Count == 0)
                            {
                                Console.WriteLine("No accounts available. Please create an account first.");
                            }
                            else
                            {
                                Console.Write("Enter Account Number: ");
                                if (double.TryParse(Console.ReadLine(), out double accountNumber))
                                {
                                    OpenAccount account = accounts.Find(a => a.AccountNumber == accountNumber);
                                    if (account != null)
                                    {
                                        int incorrectPinAttempts = 0;
                                        while(incorrectPinAttempts < 3)
                                        {
                                            Console.Write("Enter your 4-digit PIN: ");
                                            if (int.TryParse(Console.ReadLine(), out int enteredPIN))
                                            {
                                                if (enteredPIN == account.Pin)
                                                {
                                                    details.viewAccountDetails(account);
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid PIN. Access denied.");
                                                    incorrectPinAttempts++;
                                                }
                                                
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid PIN format.");
                                            }
                                           
                                        }
                                        if (incorrectPinAttempts >= 3)
                                        {
                                            Console.WriteLine("Account locked due to too many incorrect PIN attempts.");
                                            Environment.Exit(0);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Account not found.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid account number input.");
                                }
                            }
                            break;
                        case 6:
                            if (accounts.Count == 0)
                            {
                                Console.WriteLine("No accounts available. Please create an account first.");
                            }
                            else
                            {
                                Console.Write("Enter Account Number: ");
                                if (double.TryParse(Console.ReadLine(), out double accountNumber))
                                {
                                    OpenAccount accountToView = accounts.Find(a => a.AccountNumber == accountNumber);
                                    if (accountToView != null)
                                    {
                                        Console.Write("Enter your 4-digit PIN: ");
                                        if (int.TryParse(Console.ReadLine(), out int enteredPIN))
                                        {
                                            if (enteredPIN == accountToView.Pin)
                                            {
                                                transactionHistory.viewTransactionHistory(accountToView);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Invalid PIN. Access denied.");
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid PIN format.");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Account not found.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid account number input.");
                                }
                            }
                            break;
                       
                        case 7:
                            Environment.Exit(0); // Exit the program
                            break;
                        default:
                            Console.WriteLine("You have entered a wrong input");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }
    }
}

