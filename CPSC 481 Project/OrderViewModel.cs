using CPSC_481_Project.Annotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace CPSC_481_Project
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        private readonly Window _owner;
        private List<Person> _peoplesCurrentOrders;

        public ICommand AddPerson => _addPerson;
        private RelayCommand _addPerson;
        public event PersonChanged SelectedPersonChanged;

        private Person _selectedPerson;

        [CanBeNull]
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                SelectedPersonChanged?.Invoke(value);
            }
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

        public ICommand ConfirmCommand => _confirmCommand;
        private RelayCommand _confirmCommand;
        public OrderViewModel(Window owner)
        {
            _addPerson = new RelayCommand(AddPersonPressed);
            _owner = owner;
            _peoplesCurrentOrders = new List<Person>();
            var p = new Person("Person 1");
            p.CanDelete = false;
            _peoplesCurrentOrders.Add(p);
            SelectedPerson = p;


            _confirmCommand =
            new RelayCommand(Confirmed, (_) => People.Any(x => x.Ordering.OrderInformation.Count > 0));

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

            if (_personCount == 1)
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
            Keyboard?.Invoke(value);

            foreach (var x in _peoplesCurrentOrders)
            {
                x.CanEdit = value;
            }
            OnPropertyChanged(nameof(People));
            SelectedPerson = _selectedPerson;
        }

        public void AddToOrder(FoodItemView item)
        {
            if (SelectedPerson is null)
                return;
            _peoplesCurrentOrders.First(x => x.Name.Equals(SelectedPerson.Name)).Ordering.AddItemToOrder(item);
            OnPropertyChanged(nameof(People));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Confirmed(object obj)
        {
            foreach (var person in People)
            {
                foreach (var item in person.Ordering.OrderInformation)
                {
                    person.Ordered.AddItemToOrder(item.View);
                }

                person.Ordering.Clear();
                OnPropertyChanged(nameof(People));
            }
        }

        public event BoolEvent Keyboard;
    }

    public delegate void PersonChanged(Person p);
}