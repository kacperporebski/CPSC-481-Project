using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class OrderInformation
    {
        public string Item { get; }
        public double Price { get; }

        public OrderInformation(FoodItemView view, string s, double p)
        {
            View = view;
            Item = s;
            Price = p;

            ExtraInformation = GetInfo(view);
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
        
    }
}