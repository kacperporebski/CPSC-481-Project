using System;
using System.ComponentModel;
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
                FoodList.UpdateFilters(GlutenChecked, LactoseChecked, PeanutChecked, NotSpicyChecked);
                OnPropertyChanged(nameof(NumFiltersSelected));
            }
        }

        public bool LactoseChecked {
            get => _lactoseChecked;
            set
            {
                _lactoseChecked = value;
                FoodList.UpdateFilters(GlutenChecked, LactoseChecked, PeanutChecked, NotSpicyChecked);
                OnPropertyChanged(nameof(NumFiltersSelected));
            }
        }
        public bool PeanutChecked {
            get => _peanutChecked;
            set
            {
                _peanutChecked = value;
                FoodList.UpdateFilters(GlutenChecked, LactoseChecked, PeanutChecked, NotSpicyChecked);
                OnPropertyChanged(nameof(NumFiltersSelected));
            }
        }
        public bool NotSpicyChecked {
            get => _notSpicyChecked;
            set
            {
                _notSpicyChecked = value;
                FoodList.UpdateFilters(GlutenChecked, LactoseChecked, PeanutChecked, NotSpicyChecked);
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
}
