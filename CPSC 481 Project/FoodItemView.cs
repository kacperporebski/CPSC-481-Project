using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class FoodItemView: INotifyPropertyChanged
    {
        public ObservableCollection<Ingredient> Ingredients { get; }
        public ObservableCollection<Substitute> Substitutes { get; }
        
        public List<Filter> Filters { get; }

        public Visibility CookingOptionsEnabled { get; }

        private CookingPref _currentPref;

        public string CookingPreference
        {
            get
            {
                return _currentPref switch
                {
                    CookingPref.Rare => "Rare          ",
                    CookingPref.MediumRare => "Medium Rare",
                    CookingPref.Medium => "Medium         ",
                    CookingPref.MediumWell => "Medium Well",
                    CookingPref.WellDone => "Well Done    ",
                    _ => throw new ArgumentOutOfRangeException(),
                };
            }
        }

        private string _path;
        private string _name;
        private string _price;
        private string _description;
        [CanBeNull] public readonly string ShortName;
        public string ImagePath => _path;
        public string Name => _name;
        public string Price => _price;
        public string Description => _description;
        
        public ICommand ConfigureCommand => _configureCommand;
        private RelayCommand _configureCommand;
        public ICommand AddCommand => _addCommand;
        private RelayCommand _addCommand;

        public string SelectedPerson { get; set; }


        public FoodItemView(string path, string name, string price, string description, [CanBeNull] string shortName = null, List<string> ingredients = null, List<string> subs = null, bool cookingPreference = false, [CanBeNull] List<Filter> filters = null)
        {
            Filters = filters ?? new List<Filter>();
            _commandI = new RelayCommand(IncreaseQuantity);
            _commandD = new RelayCommand(DecreaseQuantity);
            _description = description;
            ShortName = shortName;
            Ingredients = new ObservableCollection<Ingredient>();
            SelectedPerson = "Person 1 Selected";
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

            if (cookingPreference)
            {
                _currentPref = CookingPref.Medium;
                CookingOptionsEnabled = Visibility.Visible;
            }
            else
            {
                CookingOptionsEnabled = Visibility.Collapsed;
            }

            _path = path;
            _name = name;
            _price = price;
            _configureCommand = new RelayCommand(ButtonPressed);
            _addCommand = new RelayCommand(AddToOrder);
        }


        public ICommand Increase => _commandI;
        public ICommand Decrease => _commandD;
        private RelayCommand _commandI;
        private RelayCommand _commandD;
        private void IncreaseQuantity(object obj)
        {
            if(_currentPref == CookingPref.WellDone)
                return;

            _currentPref++;
            OnPropertyChanged(nameof(CookingPreference));
        }

        private void DecreaseQuantity(object obj)
        {
            if (_currentPref == CookingPref.Rare)
                return;

            _currentPref--;
            OnPropertyChanged(nameof(CookingPreference));
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
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateSelectedPerson(string person)
        {
            SelectedPerson = person;
            OnPropertyChanged(nameof(SelectedPerson));
        }
    }

    public enum Filter
    {
        LactoseFree,
        GlutenFree,
        PeanutFree,
        NotSpicy,
    }

    public delegate void FoodItemChanged(FoodItemView item);
}