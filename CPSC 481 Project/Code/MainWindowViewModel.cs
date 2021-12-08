﻿using System;
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

        public Visibility Keyboard { get; set; }
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
        public ICommand CartButtonCommand =>_cartButtonCommand;
        public event Action CartButtonEvent;

        private RelayCommand _cartButtonCommand;

        private bool _isCallingServer;
        private string _callServerText = "C A L L  S E R V E R";
        
        public MainWindowViewModel(Window owner)
        {
            Keyboard = Visibility.Hidden;
            FoodList = new FoodListViewModel();
            OrderModel = new OrderViewModel(owner);
            OrderModel.Keyboard += UpdateKeyboardView;
            OrderModel.SelectedPersonChanged += FoodList.UpdateSelectedPerson;
            _callServerCommand = new RelayCommand(CallingServer, (_) => !_isCallingServer);
            _cartButtonCommand = new RelayCommand(UpdateVisibility, OrderModel.HasSomethingHasBeenOrdered);
        }

        private void UpdateVisibility(object obj)
        {
            CartButtonEvent?.Invoke();
        }

        private void UpdateKeyboardView(bool value)
        {
            if (value) Keyboard = Visibility.Visible;
            else
            {
                Keyboard = Visibility.Hidden;
            }
            OnPropertyChanged(nameof(Keyboard));
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
