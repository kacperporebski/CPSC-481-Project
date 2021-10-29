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

namespace CPSC_481_Project
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CategoriesDropDown : UserControl
    {
        public CategoriesDropDown()
        {
            InitializeComponent();
        }

        private void MainButton_OnClick(object sender, RoutedEventArgs e)
        {
            Box.Visibility = Box.Visibility is Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            Box2.Visibility = Box2.Visibility is Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new Window1();
            window.ShowDialog();
        }
        
    }
}
