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
using System.Windows.Shapes;

namespace Bank
{
    /// <summary>
    /// Interakční logika pro PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        private BankAccount sender;
        private BankAccount reciever;

        public PaymentWindow(BankAccount sender, BankAccount reciever)
        {
            InitializeComponent();
            this.sender = sender;
            this.reciever = reciever;
            senderNumberLabel.Content = sender.AccountNumber;
            recieverNumberLabel.Content = reciever.AccountNumber;
            recieverNameLabel.Content = reciever.User.Name + " " + reciever.User.Surname;
        }

        private void amountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(decimal.TryParse(amountTextBox.Text, out decimal amountToBePaid))
            {
                if ((this.sender.Balance - amountToBePaid) >= 0)
                {
                    balanceLeftLabel.Foreground = Brushes.Green;
                }
                else
                {
                    balanceLeftLabel.Foreground = Brushes.Red;
                }
                balanceLeftLabel.Content = "You will have $" + (this.sender.Balance - amountToBePaid) + " left in you account";
            }
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(decimal.TryParse(amountTextBox.Text, out decimal amount))
            {
                this.sender.SendPayment(reciever, amount);

                using(SqlConnection connection = new SqlConnection(App.connectionString))
                {
                    connection.Open();

                    string cmd = "UPDATE BankAccounts SET Balance=@balance WHERE AccountNumber=@accountNumber";

                    SqlCommand sqlSender = new SqlCommand(cmd, connection);
                    sqlSender.Parameters.AddWithValue("@balance", this.sender.Balance);
                    sqlSender.Parameters.AddWithValue("@accountNumber", this.sender.AccountNumber);
                    sqlSender.ExecuteNonQuery();
                    SqlCommand sqlReciever = new SqlCommand(cmd, connection);
                    sqlReciever.Parameters.AddWithValue("@balance", reciever.Balance);
                    sqlReciever.Parameters.AddWithValue("@accountNumber", reciever.AccountNumber);
                    sqlReciever.ExecuteNonQuery();

                    connection.Close();
                }

                Close();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
