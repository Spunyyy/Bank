using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserTypeAccount UserType { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }

        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();


        public User()
        {
            
        }

        public User(int userId, string userName, string password, UserTypeAccount userType, string name, string surname)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            UserType = userType;
            Name = name;
            Surname = surname;
        }
    }
}
