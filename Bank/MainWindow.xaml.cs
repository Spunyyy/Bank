using Microsoft.Data.SqlClient;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User loggedUser;

        public User LoggedUser { get { return loggedUser; } }

        private static MainWindow instance;

        public List<BankAccount> BankAccounts = new List<BankAccount>();

        public MainWindow(User user)
        {
            InitializeComponent();
            instance = this;
            loggedUser = user;
            if (loggedUser.UserType == UserTypeAccount.Client)
            {
                accountsItem.Visibility = Visibility.Hidden;
            }
            LoadUserBankAccounts();
            LoadAllBankAccount();
        }

        public static MainWindow getInstance()
        {
            return instance;
        }

        private void LoadAllBankAccount()
        {
            using (SqlConnection connection = new SqlConnection(App.connectionString))
            {
                connection.Open();
                SqlCommand sql = new SqlCommand("SELECT * FROM BankAccounts", connection);
                SqlDataReader dataReader = sql.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountType accountType = AccountType.Current;
                    if (dataReader[2].ToString().Trim() == "Savings")
                    {
                        accountType = AccountType.Savings;
                    }

                    User userAccount = FindUser(dataReader.GetInt32(3));

                    BankAccounts.Add(new BankAccount(dataReader.GetString(0),
                                                                dataReader.GetDecimal(1),
                                                                accountType,
                                                                dataReader.GetInt32(3),
                                                                userAccount));
                }
                connection.Close();
            }
        }

        private void LoadUserBankAccounts()
        {
            using(SqlConnection connection = new SqlConnection(App.connectionString))
            {
                connection.Open();
                SqlCommand sql = new SqlCommand("SELECT * FROM BankAccounts WHERE UserID = @userId", connection);
                sql.Parameters.AddWithValue("@userId", loggedUser.UserId);
                SqlDataReader dataReader = sql.ExecuteReader();
                while (dataReader.Read())
                {
                    AccountType accountType = AccountType.Current;
                    if (dataReader[2].ToString().Trim() == "Savings")
                    {
                        accountType = AccountType.Savings;
                    }

                    loggedUser.BankAccounts.Add(new BankAccount(dataReader.GetString(0),
                                                                dataReader.GetDecimal(1),
                                                                accountType,
                                                                dataReader.GetInt32(3),
                                                                loggedUser));
                }
                connection.Close();
            }
        }

        private User FindUser(int userId)
        {
            foreach(User user in App.Users)
            {
                if(user.UserId == userId)
                {
                    return user;
                }
            }
            return null;
        }

        private void addAccountItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new NewUser();
        }

        private void viewAccountItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ViewUsers();
        }

        private void mainPageItem_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new MainPage();
        }

        private void logoutItem_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow window = new LoginWindow();
            window.Show();
            loggedUser.BankAccounts.Clear();
            Close();
        }
    }
}