using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class OrderSummary:INotifyPropertyChanged
    {
        private List<OrderInformation> _orderInformation = new ();
        public ObservableCollection<OrderInformation> OrderInformation {
            get
            {
                var collection = new ObservableCollection<OrderInformation>();
                foreach (var order in _orderInformation)
                {
                    collection.Add(order);
                }
                return collection;
            }
        }

        public OrderSummary()
        {
        }

        public void Clear()
        {
            _orderInformation = new();
        }

        public void AddItemToOrder(FoodItemView item)
        {
            _orderInformation.Add(item.ShortName is not null
                ? new OrderInformation(item, item.ShortName, Double.Parse(item.Price.Split('$')[1]))
                : new OrderInformation(item, item.Name, Double.Parse(item.Price.Split('$')[1])));
            OnPropertyChanged(nameof(OrderInformation));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}