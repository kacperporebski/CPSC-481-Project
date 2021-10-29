using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    class MainWindowViewModel: INotifyPropertyChanged
    {
        public OrderViewModel OrderModel { get; }
        public FoodListViewModel FoodList { get; }

        public int NumFiltersSelected
        {
            get
            {
                var num = 0;
                if(GlutenChecked is true) num++;
                if(LactoseChecked is true) num++;
                if(PeanutChecked is true) num++;
                if(NotSpicyChecked is true) num++;
                return num;
            }
        }

        private bool _glutenChecked;
        private bool _lactoseChecked;
        private bool _peanutChecked;
        private bool _notSpicyChecked;
        public bool GlutenChecked
        {
            get => _glutenChecked;
            set
            {
                _glutenChecked = value;
                OnPropertyChanged(nameof(NumFiltersSelected));
            }
        }

        public bool LactoseChecked {
            get => _lactoseChecked;
            set
            {
                _lactoseChecked = value;
                OnPropertyChanged(nameof(NumFiltersSelected));
            }
        }
        public bool PeanutChecked {
            get => _peanutChecked;
            set
            {
                _peanutChecked = value;
                OnPropertyChanged(nameof(NumFiltersSelected));
            }
        }
        public bool NotSpicyChecked {
            get => _notSpicyChecked;
            set
            {
                _notSpicyChecked = value;
                OnPropertyChanged(nameof(NumFiltersSelected));
            }
        }

        public string CallServerText => _callServerText;

        public ICommand CallServerCommand => _callServerCommand;
        private RelayCommand _callServerCommand;
        private bool _isCallingServer;
        private string _callServerText = "Call Server";

        public MainWindowViewModel()
        {
            FoodList = new FoodListViewModel();
            _callServerCommand = new RelayCommand(CallingServer, (_) => !_isCallingServer);
            OrderModel = new OrderViewModel();
        }

        private async void CallingServer(object obj)
        {
            _isCallingServer = true;
            _callServerText = "Staff OTW";
            OnPropertyChanged(nameof(CallServerText));
            await TimerStart();
            _isCallingServer = false;
            _callServerText = "Call Server";
            OnPropertyChanged(nameof(CallServerText));
            CommandManager.InvalidateRequerySuggested();
        }

        private async Task TimerStart()
        {
            var timer = new System.Timers.Timer(4000);
            timer.Start();
            timer.AutoReset = false;
            var done = false;
            timer.Elapsed += (_,_) =>
            {
                done = true;
            };

            await Task.Run(() =>
            {
                while (!done) Thread.Sleep(10);
            });
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddItemToOrder(FoodItemView item)
        {
            OrderModel.AddToOrder(item);
        }
    }

    public class OrderViewModel:INotifyPropertyChanged
    {
        private List<Person> _peoplesOrders;

        [CanBeNull]
        public Person SelectedPerson { 
            get; 
            set;
        }
        
        public ObservableCollection<Person> People
        {
            get
            {
                var collection = new ObservableCollection<Person>();
                foreach (var person in _peoplesOrders)
                {
                    collection.Add(person);
                }

                return collection;
            }
        }

        public OrderViewModel()
        {
            _peoplesOrders = new List<Person>();
            _peoplesOrders.Add(new Person("Person 1"));
            _peoplesOrders.Add(new Person("Person 2"));
            _peoplesOrders.Add(new Person("Person 3"));
            _peoplesOrders.Add(new Person("Person 4"));
            _peoplesOrders.Add(new Person("Person 5"));
        }

        public void AddToOrder(FoodItemView item)
        {
            if(SelectedPerson is null)
                return;
            _peoplesOrders.First(x => x.Name.Equals(SelectedPerson.Name)).Order.AddItemToOrder(item);
            OnPropertyChanged(nameof(People));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
