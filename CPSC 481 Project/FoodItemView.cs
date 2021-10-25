using System.Net.Mime;
using System.Windows.Input;

namespace CPSC_481_Project
{
    public class FoodItemView
    {
        private string _path;
        private string _name;
        private string _price;
        public string ImagePath => _path;
        public string Name => _name;
        public string Price => _price;

        public ICommand Button => _button;
        private RelayCommand _button;
       

        public FoodItemView(string path, string name, string price)
        {
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

    public delegate void FoodItemOrdered(FoodItemView item);

    public interface IFoodItemView
    {
        public string ImagePath { get; }
        public string Name { get; }
        public string Price { get; }
    }
}