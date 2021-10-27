using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class FoodItemView
    {
        public ObservableCollection<Substitute> Substitutes { get; }
        private string _path;
        private string _name;
        private string _price;
        public string ImagePath => _path;
        public string Name => _name;
        public string Price => _price;

        public ICommand Button => _button;
        private RelayCommand _button;


        private int _quantity;
        public int Quantity => _quantity;

        public FoodItemView(string path, string name, string price, List<string> substitutes = null)
        {
            Substitutes = new ObservableCollection<Substitute>();
            if (substitutes is not null)
                foreach (var s in substitutes)
                {
                    Substitutes.Add(new Substitute(s));
                }

            _path = path;
            _name = name;
            _price = price;
            _button = new RelayCommand(ButtonPressed);

        }

        private void ButtonPressed(object obj)
        {
            Ordered!.Invoke(this);
        }

        public event FoodItemOrdered Ordered;
    }

    public class Substitute:INotifyPropertyChanged
    {
        public string Description { get; }
        public IngredientQuantity Quantity => _quantity;
        public ICommand Increase => _commandI;
        public ICommand Decrease => _commandD;
        private RelayCommand _commandI;
        private RelayCommand _commandD;
        private IngredientQuantity _quantity;

        public Substitute(string description)
        {
            Description = description;
            _quantity = IngredientQuantity.Normal;
            _commandI = new RelayCommand(IncreaseQuantity);
            _commandD = new RelayCommand(DecreaseQuantity);
        }

        private void IncreaseQuantity(object obj)
        {
            _quantity++;
            OnPropertyChanged(nameof(Quantity));
        }

        private void DecreaseQuantity(object obj)
        {
            _quantity--;
            OnPropertyChanged(nameof(Quantity));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public delegate void FoodItemOrdered(FoodItemView item);

    public enum IngredientQuantity
    {
        None,
        Light,
        Normal,
        Medium,
        Extra
    }
}