using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lab2
{
    class People : INotifyPropertyChanged {
        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> PeopleList {
            get { return _people; }
            set { _people = value; }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public People() {
            _people = new ObservableCollection<Person>() {
                new Person{ 
                    ID = 1, 
                    FirstName = "John", 
                    LastName = "Doe", 
                    Salary = 50000, 
                    Color = Color.Red, 
                    Address = "123 Main St", 
                    Birthday = new DateOnly(1990, 1, 1), 
                    Email = "john.doe@example.com", 
                    Notes = "No notes", 
                    PhoneNumber = "555-1234" },
                new Person{ 
                    ID = 2, 
                    FirstName = "Jane", 
                    LastName = "Smith", 
                    Salary = 60000, 
                    Color = Color.Blue, 
                    Address = "456 Elm St", 
                    Birthday = new DateOnly(1992, 2, 2), 
                    Email = "jane.smith@example.com", 
                    Notes = "No notes", 
                    PhoneNumber = "555-5678" },
                new Person{ 
                    ID = 3, 
                    FirstName = "Michael", 
                    LastName = "Johnson", 
                    Salary = 55000, 
                    Color = Color.Green, 
                    Address = "789 Oak St", 
                    Birthday = new DateOnly(1995, 3, 3), 
                    Email = "michael.johnson@example.com", 
                    Notes = "No notes", 
                    PhoneNumber = "555-8765" },
            };
        }

        public void AddPerson(Person person) {
            _people.Add(person);
            Console.WriteLine("People:\n");
            foreach (var p in _people) {
                Console.WriteLine($"{p.ID}: {p.FirstName} {p.LastName}");
            }
            OnPropertyChanged("PeopleList");
        }

        public void RemovePerson(Person person) {
            _people.Remove(person);
            OnPropertyChanged("PeopleList");
        }
    }
}