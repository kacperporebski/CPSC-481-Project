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

namespace CPSC_481_Project
{
    /// <summary>
    /// Interaction logic for FoodList.xaml
    /// </summary>
    public partial class FoodList : UserControl, INotifyPropertyChanged
    {
      public FoodList()
        {
            _foodItems = new ObservableCollection<FoodItem>();
            InitializeComponent();
            _foodItems.Add(new FoodItem());
            _foodItems.Add(new FoodItem());
            _foodItems.Add(new FoodItem());
            _foodItems.Add(new FoodItem());
            _foodItems.Add(new FoodItem());
            _foodItems.Add(new FoodItem());
            _foodItems.Add(new FoodItem());
            _foodItems.Add(new FoodItem());
            OnPropertyChanged();
            Items.ItemsSource = FoodItems;
        }

      private readonly ObservableCollection<FoodItem> _foodItems;

      public ObservableCollection<FoodItem> FoodItems => _foodItems;

      public event PropertyChangedEventHandler PropertyChanged;

      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
          PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
    }
}
