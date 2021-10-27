namespace CPSC_481_Project
{
    public class OrderInformation
    {
        public string Item { get; }
        public double Price { get; }

        public OrderInformation(string s, double p)
        {
            Item = s;
            Price = p;
        }
    }
}