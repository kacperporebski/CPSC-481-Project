using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class FoodListViewModel: INotifyPropertyChanged
    {

        private readonly Dictionary<CategoryType, List<FoodItemView>> _food;

        public ObservableCollection<Category> FoodItems
        {
            get
            {
                var collection = new ObservableCollection<Category>();
                foreach (var category in _food.Keys)
                {
                    if (_whiteList.Contains(category))
                    {
                        collection.Add(new Category(category, ParseFilters(_food[category]) ));
                    }
                }

                return collection;
            }
        }

        private IEnumerable<FoodItemView> ParseFilters(List<FoodItemView> foodItemViews)
        {
            var list = new List<FoodItemView>();
            foreach (var item in foodItemViews)
            {
                var i = CurrentFilter.Count(filter => item.Filters.Contains(filter));
                if (i == CurrentFilter.Count)
                    list.Add(item);
            }
            return list;
        }

        public ICommand AllCategories => _allCategories;
        private readonly RelayCommand _allCategories;

        public ICommand Appetizers => _appetizers;
        private readonly RelayCommand _appetizers;

        public ICommand Desserts => _desserts;
        private readonly RelayCommand _desserts;

        public ICommand Drinks => _drinks;
        private readonly RelayCommand _drinks;

        public ICommand Mains => _mains;
        private readonly RelayCommand _mains;

        public ICommand Entrees => _entrees;
        private readonly RelayCommand _entrees;

        private List<CategoryType> _whiteList;

        public List<Filter> CurrentFilter
        {
            get
            {
                var filters = new List<Filter>();
                if(_glutenChecked)
                    filters.Add(Filter.GlutenFree);
                if (_lactoseChecked)
                    filters.Add(Filter.LactoseFree);
                if (_peanutChecked)
                    filters.Add(Filter.PeanutFree);
                if (_notSpicyChecked)
                    filters.Add(Filter.NotSpicy);
                return filters;
            }
        }

        private bool _glutenChecked;
        private bool _lactoseChecked;
        private bool _peanutChecked;
        private bool _notSpicyChecked;

        public FoodListViewModel()
        {
            SetAllCategories(null);
            _allCategories = new RelayCommand(SetAllCategories);
            _appetizers = new RelayCommand(SetAppys);
            _desserts = new RelayCommand(SetDesserts);
            _drinks = new RelayCommand(SetDrinks);
            _mains = new RelayCommand(SetMains);
            _entrees = new RelayCommand(SetEntrees);



            _food = new ();
            _food.Add(CategoryType.Mains, new List<FoodItemView>
            {
                new ("Steak.jpg", "Steak", "Price: $27", "AAA New York Striploin served with mashed potatoes, grilled asparagus and mushroom sauce", new List<string>(){"Sauce", "Mashed Potatoes", "Asparagus"}, new List<string>{ "Brocoli instead of Asparagus" }, true, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree
                    }),
                new ("NashvilleChicken.jpg", "Nashviille Hot Chicken Sandwich", "Price: $22", "Hot chicken thigh, slaw, mayonnaise, on a toasted Belgian waffle", new List<string>(){"Ingredient 1", "Ingredient 2"}, new List<string>{ "Lettuce Bun" }),
                new ("R (1).jpg", "Ramen", "Price: $17", "Description goes here" , new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new ("ChickenTeriyaki.jpg", "Teriyaki Chicken Rice Bowl", "Price: $16", "Description goes here", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                new ("Spaghetti.jpg", "Spaghetti", "Price: $20", "Description goes here", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                new ("ChickenEnchiladas.jpg", "Chicken Enchiladas", "Price: $19", "Description goes here goes here", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                new ("Steak.jpg", "Steak", "Price: $27", "AAA New York Striploin served with mashed potatoes, grilled asparagus and mushroom sauce", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                
            });
            _food.Add(CategoryType.Drinks, new List<FoodItemView>
            {
                new("R (1).jpg", "Ramen", "Price: $17", "Description goes here" , new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("R (1).jpg", "Ramen", "Price: $17", "Description goes here" , new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("R (1).jpg", "Ramen", "Price: $17", "Description goes here" , new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("R (1).jpg", "Ramen", "Price: $17", "Description goes here" , new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("R (1).jpg", "Ramen", "Price: $17", "Description goes here" , new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("R (1).jpg", "Ramen", "Price: $17", "Description goes here" , new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("R (1).jpg", "Ramen", "Price: $17", "Description goes here" , new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
            });
            OnPropertyChanged();
        }

        private void SetEntrees(object obj)
        {
            _whiteList = new List<CategoryType>() {CategoryType.Entrees};
            OnPropertyChanged(nameof(FoodItems));
        }

        private void SetMains(object obj)
        {
            _whiteList = new List<CategoryType>() { CategoryType.Mains };
            OnPropertyChanged(nameof(FoodItems));
        }

        private void SetDrinks(object obj)
        {
            _whiteList = new List<CategoryType>() { CategoryType.Drinks };
            OnPropertyChanged(nameof(FoodItems));
        }

        private void SetAppys(object obj)
        {
            _whiteList = new List<CategoryType>() { CategoryType.Appetizers };
            OnPropertyChanged(nameof(FoodItems));
        }

        private void SetDesserts(object obj)
        {
            _whiteList = new List<CategoryType>() { CategoryType.Dessert };
            OnPropertyChanged(nameof(FoodItems));
        }

        private void SetAllCategories(object obj)
        {
            _whiteList = new List<CategoryType>() { CategoryType.Appetizers, CategoryType.Entrees, CategoryType.Mains, CategoryType.Drinks, CategoryType.Dessert };
            OnPropertyChanged(nameof(FoodItems));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateFilters(bool glutenChecked, bool lactoseChecked, bool peanutChecked, bool notSpicyChecked)
        {
            _glutenChecked = glutenChecked;
            _lactoseChecked = lactoseChecked;
            _peanutChecked = peanutChecked;
            _notSpicyChecked = notSpicyChecked;
            OnPropertyChanged(nameof(FoodItems));
        }
    }

    public class Category
    {
        private readonly ObservableCollection<FoodItemView> _foodItems;
        public ObservableCollection<FoodItemView> FoodItems => _foodItems;

        public CategoryType Name { get; }

        public Category(CategoryType name, IEnumerable<FoodItemView> items)
        {
            Name = name;
            _foodItems = new ObservableCollection<FoodItemView>();
            foreach (var food in items)
            {
                _foodItems.Add(food);
            }
        }

    }

    public enum CategoryType
    {
        Mains,
        Appetizers,
        Drinks,
        Entrees,
        Dessert
    }
}