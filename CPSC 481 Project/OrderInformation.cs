using System;
using System.CodeDom;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class OrderInformation
    {
        public string Item { get; }
        public double Price { get; }

        public event RemoveItem RemoveMe;

        public OrderInformation(FoodItemView view, string s, double p)
        {
            View = view;
            Item = s;
            Price = p;

            ExtraInformation = GetInfo(view);

            _removeCommand = new RelayCommand(Remove);
        }

        private void Remove(object obj)
        {
            RemoveMe?.Invoke(this);
        }

        private string GetInfo(FoodItemView view)
        {
            string info = "";

            foreach (var ingredient in view.Ingredients)
            {
                info += $"{ingredient.Description}: {ingredient.Quantity} \n";
            }

            foreach (var sub in view.Substitutes)
            {
                if (sub.Checked)
                    info += $"{sub.Name}\n";
            }

            if (view.CookingOptionsEnabled == Visibility.Visible)
            {
                info += $"{view.CookingPreference}\n";
            }

            return info;
        }

        public FoodItemView View { get; set; }

        public string ExtraInformation { get; }


        public ICommand RemoveCommand => _removeCommand;
        private readonly RelayCommand _removeCommand;

    }

    public delegate void RemoveItem(OrderInformation ItemToRemove);
}