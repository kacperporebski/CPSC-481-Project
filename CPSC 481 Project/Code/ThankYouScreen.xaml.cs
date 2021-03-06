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

namespace CPSC_481_Project
{
    /// <summary>
    /// Interaction logic for ThankYouScreen.xaml
    /// </summary>
    public partial class ThankYouScreen : Window
    {
        public ThankYouScreen()
        {
            InitializeComponent();
        }

        public event Event MainMenu;

        private void MainMenu_OnClick(object sender, RoutedEventArgs e)
        {
            MainMenu?.Invoke();
        }
    }
}
