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
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        public OrderList()
        {
            InitializeComponent();
        }


        private void Pay_OnClick(object sender, RoutedEventArgs e)
        {
            PayButton?.Invoke();
        }

        public event Event BackButton;
        public event Event PayButton;

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            BackButton?.Invoke();
        }
    }

    public delegate void Event();
}
