using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool AddingToOrder = false;
        private bool FiltersOpen = false;
        private MainWindowViewModel MainWindowData;
        
        public MainWindow()
        {
            InitializeComponent();
            ChangeVisibilities();

            MainWindowData = new MainWindowViewModel();
            foreach (var item in MainWindowData.FoodList.FoodItems)
            {
                item.Configuring += ItemConfigScreen;
                item.AddingToOrder += AddToOrder;
            }
            DataContext = MainWindowData;
            
           
        }

        private void AddToOrder(FoodItemView item)
        {
            MainWindowData.OrderSummary.AddItemToOrder(item);
            OnPropertyChanged(nameof(OrderSummary));
        }

        private void ItemConfigScreen(FoodItemView item)
        {
            AddingToOrder = true;
            ItemConfig.DataContext = item;
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

            FilterOptions.Visibility = FiltersOpen ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddingToOrder = false;
            ChangeVisibilities();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            FiltersOpen = !FiltersOpen;
            ChangeVisibilities();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class OrderSummary:INotifyPropertyChanged
    {
        private List<OrderInformation> _orderInformation = new ();
        public ObservableCollection<OrderInformation> OrderInformation {
            get
            {
                var collection = new ObservableCollection<OrderInformation>();
                foreach (var order in _orderInformation)
                {
                    collection.Add(order);
                }
                return collection;
            }
        }

        public OrderSummary()
        {
        }

        public void AddItemToOrder(FoodItemView item)
        {
            _orderInformation.Add(new OrderInformation(item.Name, Double.Parse( item.Price.Split('$')[1])));
            OnPropertyChanged(nameof(OrderInformation));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
