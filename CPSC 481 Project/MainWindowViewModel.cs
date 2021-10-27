using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    class MainWindowViewModel: INotifyPropertyChanged
    {

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

        public MainWindowViewModel()
        {
            FoodList = new FoodListViewModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
