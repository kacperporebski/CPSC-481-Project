﻿using System;
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
using System.Windows.Media;
using System.Windows.Media.Effects;
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
        private bool AddingToOrder;
        private bool FiltersOpen;
        private MainWindowViewModel MainWindowData;
        private OrderList _confirmation { get; set; }
        private ThankYouScreen _thankYouScreen { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            RestartApp();
        }

        private void RestartApp()
        {
            AddingToOrder = false;
            FiltersOpen = false;
            MainWindowData = new MainWindowViewModel(this);
            foreach (var category in MainWindowData.FoodList.FoodItems)
            {
                foreach (var item in category.FoodItems)
                {
                    item.Configuring += ItemConfigScreen;
                    item.AddingToOrder += AddToOrder;
                }

            }

            SourceInitialized += (s, a) =>
            {
                _confirmation = new OrderList();
                _confirmation.DataContext = MainWindowData.OrderModel;
                _confirmation.Owner = this;

                _thankYouScreen = new ThankYouScreen();
                _thankYouScreen.Owner = this;
            };

            DataContext = MainWindowData;
            ChangeVisibilities();
        }



        private void AddToOrder(FoodItemView item)
        {
            MainWindowData.AddItemToOrder(item);
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
                Filters.Visibility = Visibility.Hidden;
            }
            else
            {
                Categories.Visibility = Visibility.Visible;
                FoodList.Visibility = Visibility.Visible;
                ItemConfig.Visibility = Visibility.Collapsed;
                BackButton.Visibility = Visibility.Collapsed;
                Filters.Visibility = Visibility.Visible;
            }

            FilterOptions.Visibility = FiltersOpen ? Visibility.Visible : Visibility.Collapsed;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddingToOrder = false;
            ChangeVisibilities();
        }

        private void FilterButton_OnClick(object sender, RoutedEventArgs e)
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

        private void PayButton_OnClick(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Collapsed;
            _confirmation.BackButton += () =>
            {
                _confirmation.Visibility = Visibility.Collapsed;
                Visibility = Visibility.Visible;
            };
            _confirmation.PayButton += () =>
            {
                _confirmation.Visibility = Visibility.Collapsed;
                _thankYouScreen.Show();
            };
            
            _confirmation.Show();
            _thankYouScreen.MainMenu += () =>
            {
                RestartApp();
                _thankYouScreen.Visibility = Visibility.Collapsed;
                Visibility = Visibility.Visible;
            };
        }
    }

    public delegate void BoolEvent(bool update);

    public delegate void PersonEvent(Person p);

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
