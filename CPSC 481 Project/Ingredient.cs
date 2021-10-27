using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class Ingredient:INotifyPropertyChanged
    {
        public string Description { get; }
        public IngredientQuantity Quantity => _quantity;
        public ICommand Increase => _commandI;
        public ICommand Decrease => _commandD;
        private RelayCommand _commandI;
        private RelayCommand _commandD;
        private IngredientQuantity _quantity;

        public Ingredient(string description)
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

    public class Substitute:INotifyPropertyChanged
    {
        public string Name { get; }
        public bool Checked { get; set; }

        public Substitute(string name)
        {
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}