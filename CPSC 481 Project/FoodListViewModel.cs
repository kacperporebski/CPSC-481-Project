﻿using System.Collections.Generic;
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
            _food.Add(CategoryType.Entrees, new List<FoodItemView>
            {
                new ("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                new ("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                new ("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                new ("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                new ("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                new ("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),
                new ("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\Steak.jpg", "steak", "Price: $27", new List<string>(){"Broccoli", "Mashed Potatoes"}, new List<string>{ "Lettuce Bun" }),

            });
            _food.Add(CategoryType.Drinks, new List<FoodItemView>
            {
                new("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\R (1).jpg", "Ramen", "Price: $17", new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\R (1).jpg", "Ramen", "Price: $17", new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\R (1).jpg", "Ramen", "Price: $17", new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\R (1).jpg", "Ramen", "Price: $17", new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\R (1).jpg", "Ramen", "Price: $17", new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\R (1).jpg", "Ramen", "Price: $17", new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),
                new("C:\\Users\\kpWork\\source\\repos\\CPSC 481 Project\\CPSC 481 Project\\R (1).jpg", "Ramen", "Price: $17", new List<string>(){"Ramen", "Broth"}, new List<string>(){"Egg"}),

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
        Appetizers,
        Drinks,
        Entrees,
    }
}