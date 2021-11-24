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

            _food = new ();
            _food.Add(CategoryType.Mains, new List<FoodItemView>
            {
                new ("-Steak.jpg", "Steak", "Price: $27", "AAA New York Striploin served with mashed potatoes, grilled asparagus and mushroom sauce", "Steak", new List<string>(){"Sauce", "Mashed Potatoes", "Asparagus"}, new List<string>{ "Brocoli instead of Asparagus" }, true, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy
                    }),
                new ("-NashvilleChicken.jpg", "Nashville Hot Chicken Sandwich", "Price: $22", "Hot chicken thigh, slaw, mayonnaise, on a toasted Belgian waffle","NashV Chicken", new List<string>(){"Coleslaw", "Mayonnaise", "Waffle Bun"}, new List<string>{ "Lettuce Bun" },false,new List<Filter>
                    {
                        Filter.LactoseFree, Filter.PeanutFree

                    }),
                new ("-R (1).jpg", "Ramen", "Price: $17", "Chicken ramen, mung bean sprouts, green onions, and boiled egg in warm homemade broth" ,null, new List<string>(){"Noodles", "Broth","Mung Bean Sprouts","Green Onions","Boiled Egg"}, null, false, new List<Filter>
                {
                    Filter.PeanutFree, Filter.NotSpicy

                }),
                new ("-ChickenTeriyaki.jpg", "Teriyaki Chicken Rice Bowl", "Price: $16", "Sesame marinated chicken stir fried with broccoli on a bed of rice","Teriyaki Bowl", new List<string>(){"Broccoli", "Rice","Sesame Seeds"}, null,false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new ("-spaghetti.jpg", "Spaghetti", "Price: $20", "Spaghetti in homemade tomato sauce",null, new List<string>(){"Tomato Sauce"}, null, false, new List<Filter>
                    {
                        Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new ("-enchiladas.jpg", "Chicken Enchiladas", "Price: $19", "Sauteed chicken, green chiles, onions, beans, and shredded mozzarella cheese","Chicken Ench", new List<string>(){"Chicken","Green Chiles","Onions","Beans","Cheese"}, null, false, new List<Filter>
                {
                    Filter.PeanutFree

                }),
                new ("-tunapoke.jpg", "Ahi Tuna Poke Bowl", "Price: $17", "Fresh ahi tuna, sushi rice, avocado, mango, cilantro, topped with spicy mayonnaise", "Tuna Poke",new List<string>(){"Tuna", "Sushi Rice","Avocado","Mango","Cilantro","Spicy Mayo"}, new List<string>{ "Cauliflower Rice","Brown Rice" },false,new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree

                    }),
                new ("-curry.jpg", "Chicken Curry", "Price: $20.25", "Japanese chicken coconut milk based-curry with chicken thighs, carrots, potatoes, and onions, with a bed of rice ",null, new List<string>(){"Chicken", "Sushi Rice","Carrot","Potato","Onion","Rice"}, new List<string>{ "Tofu instead of chicken"},false,new List<Filter>
                    {
                        Filter.GlutenFree, Filter.PeanutFree

                    }),
            });
            _food.Add(CategoryType.Appetizers, new List<FoodItemView>
            {
                new("-chickenwings.jpg", "Buffalo Chicken Wings", "Price: $15.00", "Crispy baked chicken wings in buffalo sauce with a side of parmesan dip and celery", "BF Wings", null, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.PeanutFree

                    }),
                new("-fries.jpg", "French Fries", "Price: $11.25", "Crispy french fries with a side of ketchup",null,  new List<string>(){"Salt"}, null, false, new List<Filter>
                    {
                        Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy // Not gluten free because the fryer could be contaminated with stuff that has gluten?

                    }),
                new("-yamfries.jpg", "Yam Fries", "Price: $11.25", "Sweet yam fries topped with oregano",null, new List<string>(){"Salt", "Oregano"}, null, false, new List<Filter>
                    {
                        Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-garlicbread.jpg", "Garlic Bread", "Price: $5.25", "Oven toasted baguette slices with extra virgin olive oil, parmesan, and herbs",null, null, null, false, new List<Filter>
                    {
                       Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-potstickers.jpg", "Potstickers", "Price: $5.65", "Steamed dumplings filled with ground pork, napa cabbage, and mushroom",null, null, null, false, new List<Filter>
                    {
                        Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-calamari.jpeg", "Calamari", "Price: $13.75", "Deep fried calamari with ketchup",null, null, null, false, new List<Filter>
                    {
                        Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),

            });
            _food.Add(CategoryType.Drinks, new List<FoodItemView>
            {
                new("-icedtea.jpg", "Iced Tea", "Price: $3.25", "355ml can",null, null, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-pepsi.jpg", "Pepsi", "Price: $3.25", "355ml can",null, null, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-rootbeer.jpg", "Root Beer", "Price: $3.25", "355ml can",null, null, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-7up.jpg", "7 Up", "Price: $3.25", "355ml can",null, null, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-dasani.jpg", "Dasani Water", "Price: $3.00", "591ml bottle",null, null, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-applejuice.jpg", "Apple Juice", "Price: $4.50", "Homemade apple juice",null, null, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-orangejuice.jpg", "Orange Juice", "Price: $4.50", "Homemade orange juice",null, null, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-coffee.jpg", "Hot Coffee", "Price: $3.95", "Medium roast coffee with cream and sugar",null, new List<string>(){"Cream", "Sugar"}, new List<string>(){"Milk instead of cream","Honey instead of sugar"}, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-hotchocolate.jpg", "Hot Chocolate", "Price: $4.20", "Extra thick and creamy hot chocolate",null, new List<string>(){"Chocolate Syrup"}, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
            });
            _food.Add(CategoryType.Dessert, new List<FoodItemView>
            {
                new("-gticecream.jpg", "Green Tea Ice Cream", "Price: $4.75", "Creamy matcha flavoured ice cream","GT Ice cream", null, null, false, new List<Filter>
                    {
                        Filter.GlutenFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-cheesecake.jpg", "Raspberry New York Cheesecake", "Price: $6.25", "New York styled cheesecake slice and graham cracker crust topped with raspberry swirl", "NY Cheesecake", null, null, false, new List<Filter>
                    {
                        Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-pie.jpg", "Key Lime Pie", "Price: $5.25", "Fresh limes, graham cracker crust and hand whipped cream",null, new List<string>(){"Whipped Cream"}, null, false, new List<Filter>
                    {
                        Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-tiramisu.jpg", "Tiramisu", "Price: $6.25", "Layered tiramisu cake",null, null, null, false, new List<Filter>
                    {
                        Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-castella.jpg", "Castella Cake", "Price: $6.15", "Sweet and moist sponge cake", null, null, null, false, new List<Filter>
                    {
                       Filter.LactoseFree, Filter.PeanutFree, Filter.NotSpicy

                    }),
                new("-peanutpie.jpg", "Peanut Pecan Pie", "Price: $6.15", "Sweet peanut butter and pecan pie", null, null, null, false, new List<Filter>
                    {
                         Filter.NotSpicy

                    }),


            });
            OnPropertyChanged();
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
            _whiteList = new List<CategoryType>() { CategoryType.Appetizers, CategoryType.Mains, CategoryType.Drinks, CategoryType.Dessert };
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

        public void UpdateSelectedPerson(Person p)
        {
            foreach (var list in _food.Values)
            {
                foreach (var item in list)
                {
                    item.UpdateSelectedPerson($"{p.Name} is Selected");
                }
            }
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
        Dessert
    }
}