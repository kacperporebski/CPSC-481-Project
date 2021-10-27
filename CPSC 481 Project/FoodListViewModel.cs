using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class FoodListViewModel: INotifyPropertyChanged
    {

        private readonly ObservableCollection<FoodItemView> _foodItems;

        public ObservableCollection<FoodItemView> FoodItems => _foodItems;

        public FoodListViewModel()
        {
            _foodItems = new ObservableCollection<FoodItemView>();
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            _foodItems.Add(new FoodItemView("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27"));
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}