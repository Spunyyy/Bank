using System.Configuration;
using System.Windows;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Input;

namespace Bank
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string connectionString;
        public static List<User> Users = new List<User>(); 

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = @"(LocalDB)\MSSQLLocalDB";
            csb.InitialCatalog = "Bank";
            csb.IntegratedSecurity = true;
            connectionString = csb.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (SqlConnection connection = new SqlConnection(App.connectionString))
            {
                connection.Open();
                SqlCommand sql = new SqlCommand("SELECT UserId, Username, Password, Name, Surname, AccountType FROM Users", connection);
                SqlDataReader dataReader = sql.ExecuteReader();
                while (dataReader.Read())
                {
                    UserTypeAccount userType;
                    if (dataReader[5].ToString().Trim() == "Admin")
                    {
                        userType = UserTypeAccount.Admin;
                    }
                    else
                    {
                        userType = UserTypeAccount.Client;
                    }

                    Users.Add(new User(dataReader.GetInt32(0),
                                        dataReader.GetString(1).Trim(),
                                        dataReader.GetString(2).Trim(),
                                        userType,
                                        dataReader.GetString(3).Trim(),
                                        dataReader.GetString(4).Trim()));
                }
                connection.Close();
            }
        }
    }

}
