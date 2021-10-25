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
    /// Interaction logic for OrderInfo.xaml
    /// </summary>
    public partial class OrderInfo : UserControl, INotifyPropertyChanged
    {
        public ObservableCollection<OrderInformation> CurrentOrder => _currentOrder;
        private ObservableCollection<OrderInformation> _currentOrder;

        public OrderInfo()
        {
            _currentOrder = new ObservableCollection<OrderInformation>();
            InitializeComponent();
            OrderInformation.ItemsSource = CurrentOrder;
        }

        public void AddToOrder(FoodItem item)
        {
            _currentOrder.Add(new OrderInformation(item.Item.Text, double.Parse(item.Price.Text.Split('$')[1])));
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class OrderInformation
    {
        public string Item { get; }
        public double Price { get; }

        public OrderInformation(string s, double p)
        {
            Item = s;
            Price = p;
        }
    }
}
