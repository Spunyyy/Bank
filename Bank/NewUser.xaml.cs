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
    /// Interakční logika pro NewUser.xaml
    /// </summary>
    public partial class NewUser : Page
    {
        private User user = new User();
        public NewUser()
        {
            InitializeComponent();
            DataContext = user;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if(passwordBox.Password != passwordControlBox.Password)
            {
                MessageBox.Show("The password must match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            user.Password = Utils.HashString(passwordBox.Password);
            if (accountTypeComboBox.SelectedIndex == 0)
            {
                user.UserType = UserTypeAccount.Admin;
            }
            else
            {
                user.UserType = UserTypeAccount.Client;
            }

            using(SqlConnection connection = new SqlConnection(App.connectionString))
            {
                connection.Open();
                SqlCommand sql = new SqlCommand("INSERT INTO Users (Username, Password, Name, Surname, AccountType) VALUES (@username, @password, @name, @surname, @accountype)", connection);
                sql.Parameters.AddWithValue("@username", user.UserName);
                sql.Parameters.AddWithValue("@password", user.Password);
                sql.Parameters.AddWithValue("@name", user.Name);
                sql.Parameters.AddWithValue("@surname", user.Surname);
                sql.Parameters.AddWithValue("@accountype", user.UserType.ToString());
                sql.ExecuteNonQuery();

                SqlCommand getId = new SqlCommand("SELECT IDENT_CURRENT('Users')", connection);
                int id = Convert.ToInt32(getId.ExecuteScalar());
                user.UserId = id;
                connection.Close();
            }


            App.Users.Add(user);

            MainWindow.getInstance().Frame.Content = new MainPage();
        }
    }
}
