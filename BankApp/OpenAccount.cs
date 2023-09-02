using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class OpenAccount
    {
        [Required(ErrorMessage = "Fullname is required.")]
        [RegularExpression("^[A-Za-z ]*$", ErrorMessage = "Fullname should contain only characters.")]
        [StringLength(100, ErrorMessage = "Fullname should be no more than 100 characters.")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Email should not contain only characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phon number is required.")]
        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Invalid phone number.")]
        [StringLength(11, ErrorMessage = "Phone number should have a maximum length of 11 digits.")]
        public string PhoneNumber { get; set; }
        public double InitialBalance { get; set; }
        public int Pin { get; private set; }
        public int AccountNumber { get; private set; }
        public double Balance { get; set; }
        public int FailedPinAttempts { get; set; }


        public List<string> TransactionHistory { get; } = new List<string>();

        public OpenAccount()
        {
            Pin = GenerateRandomPin();
            AccountNumber = GenerateUniqueAccountNumber();
            Balance = InitialBalance;

        }

        private int GenerateRandomPin()
        {
            Random rand = new Random();
            return rand.Next(1000, 10000);
        }

        private int GenerateUniqueAccountNumber()
        {
            return new Random().Next(1000000000);
        }

    }
}
