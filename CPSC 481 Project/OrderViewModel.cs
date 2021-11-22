using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class OrderViewModel:INotifyPropertyChanged
    {
        private readonly Window _owner;
        private List<Person> _peoplesCurrentOrders;
        
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
                foreach (var person in _peoplesCurrentOrders)
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
            _peoplesCurrentOrders = new List<Person>();
            var p = new Person("Person 1");
            p.CanDelete = false;
            _peoplesCurrentOrders.Add(p);
            SelectedPerson = p;

            UpdateModel();
            
        }

        private void UpdateModel()
        {
            foreach (var x in _peoplesCurrentOrders)
            {
                x.SetOwner(_owner);
                x.Editting += UpdateEdittingCanExecute;
                x.Deleting += DeletePerson;
            }
        }

        private void DeletePerson(Person p)
        {
            _peoplesCurrentOrders.Remove(p);
            _personCount--;

            if(_personCount==1)
                _peoplesCurrentOrders.First().CanDelete = false;

            p.Deleting -= DeletePerson;
            p.Editting -= UpdateEdittingCanExecute;
            OnPropertyChanged(nameof(People));

        }

        private void AddPersonPressed(object _)
        {
            _personCount++;
            _personNumToAppend++;
            if (_personCount == 2)
                _peoplesCurrentOrders.First().CanDelete = true;
            var p = new Person("Person " + _personNumToAppend);
            _peoplesCurrentOrders.Add(p);
            p.Deleting += DeletePerson;
            p.Editting += UpdateEdittingCanExecute;
            OnPropertyChanged(nameof(People));
        }

        private void UpdateEdittingCanExecute(bool value)
        {
            foreach (var x in _peoplesCurrentOrders)
            {
                x.CanEdit = value;
            }
            OnPropertyChanged(nameof(People));
        }

        public void AddToOrder(FoodItemView item)
        {
            if(SelectedPerson is null)
                return;
            _peoplesCurrentOrders.First(x => x.Name.Equals(SelectedPerson.Name)).Order.AddItemToOrder(item);
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