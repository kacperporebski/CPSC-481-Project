using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CPSC_481_Project
{
    public class FoodItemView
    {
        public ObservableCollection<Ingredient> Ingredients { get; }
        public ObservableCollection<Substitute> Substitutes { get; }
        private string _path;
        private string _name;
        private string _price;
        public string ImagePath => _path;
        public string Name => _name;
        public string Price => _price;

        public ICommand ConfigureCommand => _configureCommand;
        private RelayCommand _configureCommand;
        public ICommand AddCommand => _addCommand;
        private RelayCommand _addCommand;

        private int _quantity;
        public int Quantity => _quantity;


        public FoodItemView(string path, string name, string price, List<string> ingredients = null, List<string> subs = null)
        {
            Ingredients = new ObservableCollection<Ingredient>();
            if (ingredients is not null)
                foreach (var s in ingredients)
                {
                    Ingredients.Add(new Ingredient(s));
                }

            Substitutes = new ObservableCollection<Substitute>();
            if (subs is not null)
                foreach (var s in subs)
                {
                    Substitutes.Add(new Substitute(s));
                }


            _path = path;
            _name = name;
            _price = price;
            _configureCommand = new RelayCommand(ButtonPressed);
            _addCommand = new RelayCommand(AddToOrder);
        }

        private void AddToOrder(object obj)
        {
            AddingToOrder?.Invoke(this);
        }

        private void ButtonPressed(object obj)
        {
            Configuring!.Invoke(this);
        }

        public event FoodItemChanged Configuring;
        public event FoodItemChanged AddingToOrder;
    }
    
    public delegate void FoodItemChanged(FoodItemView item);
}