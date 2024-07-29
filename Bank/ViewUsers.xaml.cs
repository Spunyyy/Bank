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
    /// Interakční logika pro ViewUsers.xaml
    /// </summary>
    public partial class ViewUsers : Page
    {
        public ViewUsers()
        {
            InitializeComponent();
            usersListView.ItemsSource = App.Users;
        }
    }
}
