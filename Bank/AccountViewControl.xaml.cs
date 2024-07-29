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
    /// Interakční logika pro AccountViewControl.xaml
    /// </summary>
    public partial class AccountViewControl : UserControl
    {
        private BankAccount account;

        public AccountViewControl(BankAccount account)
        {
            InitializeComponent();
            this.account = account;
            DataContext = account;
        }

        private void OnDetailsClick(object sender, RoutedEventArgs e)
        {
            MainWindow.getInstance().Frame.Content = new BankDetails(account);
        }
    }
}
