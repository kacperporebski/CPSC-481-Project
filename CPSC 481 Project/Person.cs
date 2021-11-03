using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CPSC_481_Project.Annotations;

namespace CPSC_481_Project
{
    public class Person:INotifyPropertyChanged
    {
        public string Name { get; set; }
        public bool CanDelete { get; set; } = true;
        public OrderSummary Order { get; }
        public bool CanEdit;
        private Window _owner;
        public ICommand RemovePerson => _removePerson;
        public RelayCommand _removePerson;
        public ICommand EditPersonName => _editPersonName;
        private RelayCommand _editPersonName;


        public Person(string name)
        {
            Name = name;
            Order = new OrderSummary();
            _editPersonName = new RelayCommand(EditName, (_) => !CanEdit);
            _removePerson = new RelayCommand(RemovePersonClicked, (_) =>  CanDelete);
        }

        private void RemovePersonClicked(object _)
        {
            Deleting?.Invoke(this);
        }

        public void SetOwner(Window owner)
        {
            _owner = owner;
        }

        private void EditName(object obj)
        {
            CanEdit = true;
            var dialog = new MyDialog("Change Name", "Enter the name for this person");
            dialog.Owner = _owner;
            Editting?.Invoke(true);
            dialog.Show();
           
            dialog.Closing += (sender, e) =>
            {
                //do stuff with input
                var dialog = (MyDialog) sender;
                if (!dialog.Canceled)
                     Name = dialog.InputText;
                CanEdit = false;
                Editting?.Invoke(false);
            };
        }

        public event PersonEvent Deleting;
        public event BoolEvent Editting;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}