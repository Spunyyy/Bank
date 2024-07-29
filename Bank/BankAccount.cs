using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bank
{
    public class BankAccount
    {
        [Key]
        public string AccountNumber { get; set; } 
        public decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public BankAccount()
        {
            
        }

        public BankAccount(string accountNumber, decimal balance, AccountType accountType, int userId, User user)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            AccountType = accountType;
            UserId = userId;
            User = user;
        }

        public void SendPayment(BankAccount reciever, decimal sum)
        {
            if((Balance - sum) <= 0)
            {
                return;
            }
            if(AccountNumber == reciever.AccountNumber)
            {
                return;
            }
            Balance -= sum;
            reciever.Balance += sum;
            MessageBox.Show("You successfully sent $" + sum + "\nreciever: " + reciever.User.Name + " " + reciever.User.Surname, "Confirmation", MessageBoxButtons.OK);
        }

        public override bool Equals(object? obj)
        {
            return obj is BankAccount account &&
                   AccountNumber == account.AccountNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AccountNumber);
        }
    }
}
