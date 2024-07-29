using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bank
{
    /// <summary>
    /// Interakční logika pro MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private User loggedUser = MainWindow.getInstance().LoggedUser;
        public MainPage()
        {
            InitializeComponent();
            AddNewAccountView();
            foreach (BankAccount account in loggedUser.BankAccounts)
            {
                AccountViewControl accountView = new AccountViewControl(account);
                AccountsPanel.Children.Insert(AccountsPanel.Children.Count - 1, accountView);
            }
        }

        private void AddAccount(string accountNumber, decimal balance, AccountType accountType)
        {
            BankAccount account = new BankAccount(accountNumber, balance, accountType, loggedUser.UserId, loggedUser);
            AccountViewControl accountView = new AccountViewControl(account);

            using(SqlConnection connection = new SqlConnection(App.connectionString))
            {
                connection.Open();
                SqlCommand sql = new SqlCommand("INSERT INTO BankAccounts (AccountNumber, Balance, AccountType, UserId) VALUES (@accountNumber, @balance, @accountType, @userId)", connection);
                sql.Parameters.AddWithValue("@accountNumber", account.AccountNumber);
                sql.Parameters.AddWithValue("@balance", account.Balance);
                sql.Parameters.AddWithValue("@accountType", account.AccountType.ToString());
                sql.Parameters.AddWithValue("@userId", account.UserId);
                sql.ExecuteNonQuery();
                connection.Close();
            }

            loggedUser.BankAccounts.Add(account);
            MainWindow.getInstance().BankAccounts.Add(account);

            AccountsPanel.Children.Insert(AccountsPanel.Children.Count - 1, accountView);
        }

        private void AddNewAccountView()
        {
            AddAccountView addAccountView = new AddAccountView();
            addAccountView.AddAccountClicked += (s, e) =>
            {
                if(addAccountView.accountTypeComboBox.SelectedIndex == 0)
                {
                    AddAccount(Utils.GenerateAccountNumber(), 0, AccountType.Current);
                }
                else if(addAccountView.accountTypeComboBox.SelectedIndex == 1)
                {
                    AddAccount(Utils.GenerateAccountNumber(), 0, AccountType.Savings);
                } 
            };
            AccountsPanel.Children.Add(addAccountView);
        }
    }
}
