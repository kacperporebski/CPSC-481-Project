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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool AddingToOrder = false;


        public MainWindow()
        {
            InitializeComponent();
            ChangeVisibilities();

            foreach (var item in FoodList.FoodItems)
            {
                item.Ordered += AddItemScreen;
            }

            
        }

        private void AddItemScreen(FoodItem item)
        {
            AddingToOrder = true;
            ChangeVisibilities();
        }

        private void ChangeVisibilities()
        {
            if (AddingToOrder)
            {
                Categories.Visibility = Visibility.Hidden;
                FoodList.Visibility = Visibility.Hidden;
                ItemConfig.Visibility = Visibility.Visible;
                BackButton.Visibility = Visibility.Visible;
            }
            else
            {
                Categories.Visibility = Visibility.Visible;
                FoodList.Visibility = Visibility.Visible;
                ItemConfig.Visibility = Visibility.Collapsed;
                BackButton.Visibility = Visibility.Collapsed;
            }
        }

        private void AddToOrder(FoodItem item)
        {
            Orders.AddToOrder(item);
            AddingToOrder = false;
            ChangeVisibilities();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddingToOrder = false;
            ChangeVisibilities();
        }
    }
}
