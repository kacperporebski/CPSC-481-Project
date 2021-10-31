using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class FoodListViewModel: INotifyPropertyChanged
    {

        private Dictionary<CategoryType, List<FoodItemView>> _food;

        public ObservableCollection<Category> FoodItems
        {
            get
            {
                var collection = new ObservableCollection<Category>();
                foreach (var category in _food.Keys)
                {
                    collection.Add(new Category(category, _food[category]));
                }

                return collection;
            }
        }

        public FoodListViewModel()
        {
            _food = new ();
            _food.Add(CategoryType.Mains, new List<FoodItemView>
            {
                new ("Steak.jpg", "Steak", "Price: $27", "AAA New York Striploin served with mashed potatoes, grilled asparagus and mushroom sauce", new List<string>(){"Sauce", "Mashed Potatoes", "Asparagus"}, new List<string>{ "Brocoli instead of Asparagus" }, true),
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Category
    {
        private ObservableCollection<FoodItemView> _foodItems;
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