using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interakční logika pro LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(userNameTextBox.Text))
            {
                MessageBox.Show("Missing username", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(userPasswordBox.Password))
            {
                MessageBox.Show("Missing password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool badData = true;

            foreach (User user in App.Users)
            {
                if ((userNameTextBox.Text == user.UserName) && (Utils.HashString(userPasswordBox.Password) == user.Password))
                {
                    MainWindow window = new MainWindow(user);
                    window.Show();
                    badData = false;
                    Close();
                    break;
                }
            }
            if (badData)
            {
                MessageBox.Show("Bad password or username", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //using(SqlConnection connection = new SqlConnection(App.connectionString))
            //{
            //    bool badData = true;

            //    connection.Open();
            //    SqlCommand sql = new SqlCommand("SELECT Username, Password FROM Users", connection);
            //    SqlDataReader dataReader = sql.ExecuteReader();
            //    while (dataReader.Read())
            //    {
            //        if((uzivatelJmenoTextBox.Text == dataReader[0].ToString().Trim()) && (HashString(uzivatelHesloPasswordBox.Password) == dataReader[1].ToString().Trim()))
            //        {
            //            connection.Close();
            //            badData = false;
            //            MainWindow window = new MainWindow();
            //            window.Show();
            //            Close();
            //            break;
            //        }
            //    }
            //    if (badData)
            //    {
            //        MessageBox.Show("Špatné heslo nebo uživatelské jméno", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}

        }

        
    }
}
