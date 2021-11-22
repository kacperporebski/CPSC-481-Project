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
        }

        public FoodItemView View { get; set; }
    }
}