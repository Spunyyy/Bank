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
    /// Interakční logika pro BankDetails.xaml
    /// </summary>
    public partial class BankDetails : Page
    {
        private List<BankAccount> bankAccounts = new List<BankAccount>(MainWindow.getInstance().BankAccounts);

        private BankAccount account;

        public BankDetails(BankAccount account)
        {
            InitializeComponent();
            this.account = account;
            accTypeLabel.Content = account.AccountType;
            accNumLabel.Content = account.AccountNumber;
            balanceLabel.Content = "$" + account.Balance;
            bankAccounts.Remove(account);
            accountsListView.ItemsSource = bankAccounts;
        }

        private void backMenuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.getInstance().Frame.Content = new MainPage();
        }

        private void accNumTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<BankAccount> wanAccounts = new List<BankAccount>();
            foreach(BankAccount account in bankAccounts)
            {
                if (account.AccountNumber.StartsWith(accNumTextBox.Text))
                {
                    wanAccounts.Add(account);
                }
            }
            accountsListView.ItemsSource = wanAccounts;
        }

        private void paymentButton_Click(object sender, RoutedEventArgs e)
        {
            if(accountsListView.SelectedItem == null)
            {
                return;
            }
            PaymentWindow window = new PaymentWindow(account, (BankAccount)accountsListView.SelectedItem);
            window.ShowDialog();
            balanceLabel.Content = "$" + account.Balance;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (account.Balance > 0)
            {
                MessageBox.Show("You can't delete account if you have more than $0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using(SqlConnection connection= new SqlConnection(App.connectionString))
            {
                connection.Open();

                SqlCommand sql = new SqlCommand("DELETE FROM BankAccounts WHERE AccountNumber=@accountNumber", connection);
                sql.Parameters.AddWithValue("@accountNumber", account.AccountNumber);
                sql.ExecuteNonQuery();

                connection.Close();
            }

            MainWindow.getInstance().LoggedUser.BankAccounts.Remove(account);
            MainWindow.getInstance().BankAccounts.Remove(account);

            MessageBox.Show("Account was deleted!", "Information", MessageBoxButton.OK);

            MainWindow.getInstance().Frame.Content = new MainPage();
        }
    }
}
