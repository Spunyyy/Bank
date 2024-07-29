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
    /// Interakční logika pro AddAccountView.xaml
    /// </summary>
    public partial class AddAccountView : UserControl
    {
        public event EventHandler AddAccountClicked;

        public AddAccountView()
        {
            InitializeComponent();
        }

        private void OnAddAccountClick(object sender, RoutedEventArgs e)
        {
            if(accountTypeComboBox.SelectedItem == null)
            {
                return;
            }
            AddAccountClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
