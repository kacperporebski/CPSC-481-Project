using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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
        private string _callServerText = "C A L L  S E R V E R";

        public MainWindowViewModel(Window owner)
        {
            FoodList = new FoodListViewModel();
            _callServerCommand = new RelayCommand(CallingServer, (_) => !_isCallingServer);
            OrderModel = new OrderViewModel(owner);
        }

        private async void CallingServer(object obj)
        {
            _isCallingServer = true;
            _callServerText = "S T A F F  O T W";
            OnPropertyChanged(nameof(CallServerText));
            await TimerStart();
            _isCallingServer = false;
            _callServerText = "C A L L  S E R V E R";
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
        private readonly Window _owner;
        private List<Person> _peoplesOrders;
        public ICommand AddPerson => _addPerson;
        private RelayCommand _addPerson;

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

        private int _personCount = 1;
        private int _personNumToAppend = 1;

        public OrderViewModel(Window owner)
        {
            _addPerson = new RelayCommand(AddPersonPressed);
            _owner = owner;
            _peoplesOrders = new List<Person>();
            var p = new Person("Person 1");
            p.CanDelete = false;
            _peoplesOrders.Add(p);

            UpdateModel();
            
        }

        private void UpdateModel()
        {
            foreach (var x in _peoplesOrders)
            {
                x.SetOwner(_owner);
                x.Editting += UpdateEdittingCanExecute;
                x.Deleting += DeletePerson;
            }
        }

        private void DeletePerson(Person p)
        {
            _peoplesOrders.Remove(p);
            _personCount--;

            if(_personCount==1)
                _peoplesOrders.First().CanDelete = false;

            p.Deleting -= DeletePerson;
            p.Editting -= UpdateEdittingCanExecute;
            OnPropertyChanged(nameof(People));

        }

        private void AddPersonPressed(object _)
        {
            _personCount++;
            _personNumToAppend++;
            if (_personCount == 2)
                _peoplesOrders.First().CanDelete = true;
            var p = new Person("Person " + _personNumToAppend);
            _peoplesOrders.Add(p);
            p.Deleting += DeletePerson;
            p.Editting += UpdateEdittingCanExecute;
            OnPropertyChanged(nameof(People));
        }

        private void UpdateEdittingCanExecute(bool value)
        {
            foreach (var x in _peoplesOrders)
            {
                x.CanEdit = value;
            }
            OnPropertyChanged(nameof(People));
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
