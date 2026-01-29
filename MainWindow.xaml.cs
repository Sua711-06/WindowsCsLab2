using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private People _peopleModel;
        private Person _backupPerson = null;

        public MainWindow()
        {
            InitializeComponent();
            _peopleModel = (People)this.FindResource("peopleModel");
            this.DataContext = _peopleModel;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true; // Email is nullable
            
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }

        private bool IsValidSalary(string salary)
        {
            if (string.IsNullOrWhiteSpace(salary))
                return true; // Salary is nullable
            
            return decimal.TryParse(salary, out _);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return true; // Phone number is nullable
            
            // Remove common separators
            string digitsOnly = System.Text.RegularExpressions.Regex.Replace(phoneNumber, @"[^\d]", "");
            
            // Check if exactly 10 digits
            return digitsOnly.Length == 10 && System.Text.RegularExpressions.Regex.IsMatch(digitsOnly, @"^\d{10}$");
        }

        private string ValidateContactData(string firstName, string lastName, string email, string salary, string phoneNumber)
        {
            // Check required fields
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return "First Name and Last Name are required.";
            
            // Validate email
            if (!IsValidEmail(email))
                return "Email must be in format: something@something.something";
            
            // Validate salary
            if (!IsValidSalary(salary))
                return "Salary must be a decimal number (e.g., 50000 or 50000.50)";
            
            // Validate phone number
            if (!IsValidPhoneNumber(phoneNumber))
                return "Phone number must be 10 digits in one of these formats:\n- 1234567890\n- 123-456-7890\n- (123)456-7890";
            
            return null; // All validations passed
        }

        private void ContactBtn_Click(object sender, RoutedEventArgs e)
        {
            // Validate all fields
            string validationError = ValidateContactData(
                FirstNameBox.Text,
                LastNameBox.Text,
                EmailBox.Text,
                SalaryBox.Text,
                PhoneNumberBox.Text
            );
            
            if (validationError != null)
            {
                MessageBox.Show(validationError, "Validation Error");
                return;
            }

            // Create new Person object from input fields
            Person newPerson = new Person
            {
                ID = _peopleModel.PeopleList.Count + 1,
                FirstName = FirstNameBox.Text,
                LastName = LastNameBox.Text,
                Email = EmailBox.Text,
                PhoneNumber = PhoneNumberBox.Text,
                Address = AddressBox.Text,
                Birthday = BirthdayBox.SelectedDate.HasValue ? DateOnly.FromDateTime(BirthdayBox.SelectedDate.Value) : DateOnly.MinValue,
                Color = ColourBox.SelectedColor.HasValue ? System.Drawing.Color.FromArgb(ColourBox.SelectedColor.Value.A, ColourBox.SelectedColor.Value.R, ColourBox.SelectedColor.Value.G, ColourBox.SelectedColor.Value.B) : System.Drawing.Color.LightGray,
                Notes = NotesBox.Text,
                Salary = SalaryBox.Text != "" && decimal.TryParse(SalaryBox.Text, out decimal salary) ? (int)salary : 0
            };

            // Add person to the collection
            _peopleModel.AddPerson(newPerson);

            MessageBox.Show("Contact added successfully!", "Success");

            // Clear input fields
            ClearInputFields();
        }

        private T FindDataContextInVisualTree<T>(DependencyObject element) where T : class
        {
            while (element != null)
            {
                if (element is FrameworkElement fe && fe.DataContext is T context)
                {
                    return context;
                }
                element = VisualTreeHelper.GetParent(element);
            }
            return null;
        }

        private void EditContactBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Person person = FindDataContextInVisualTree<Person>(button);

            if (person != null)
            {
                // Create a backup of the current state
                _backupPerson = new Person
                {
                    ID = person.ID,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    PhoneNumber = person.PhoneNumber,
                    Address = person.Address,
                    Birthday = person.Birthday,
                    Color = person.Color,
                    Notes = person.Notes,
                    Salary = person.Salary
                };

                person.IsEditing = true;
            }
        }

        private void SaveContactBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Person person = FindDataContextInVisualTree<Person>(button);

            if (person != null)
            {
                // Validate all fields
                string validationError = ValidateContactData(
                    person.FirstName,
                    person.LastName,
                    person.Email,
                    person.Salary.ToString(),
                    person.PhoneNumber
                );
                
                if (validationError != null)
                {
                    MessageBox.Show(validationError, "Validation Error");
                    return;
                }

                person.IsEditing = false;
                _backupPerson = null;
                MessageBox.Show("Contact updated successfully!", "Success");
            }
        }

        private void CancelEditBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Person person = FindDataContextInVisualTree<Person>(button);

            if (person != null && _backupPerson != null)
            {
                // Restore the original values
                person.FirstName = _backupPerson.FirstName;
                person.LastName = _backupPerson.LastName;
                person.Email = _backupPerson.Email;
                person.PhoneNumber = _backupPerson.PhoneNumber;
                person.Address = _backupPerson.Address;
                person.Birthday = _backupPerson.Birthday;
                person.Color = _backupPerson.Color;
                person.Notes = _backupPerson.Notes;
                person.Salary = _backupPerson.Salary;

                person.IsEditing = false;
                _backupPerson = null;
            }
        }

        private void DeleteContactBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Person person = FindDataContextInVisualTree<Person>(button);

            if (person != null)
            {
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {person.FirstName} {person.LastName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    _peopleModel.RemovePerson(person);
                    MessageBox.Show("Contact deleted successfully!", "Success");
                }
            }
        }

        private void ClearInputFields()
        {
            FirstNameBox.Clear();
            LastNameBox.Clear();
            EmailBox.Clear();
            PhoneNumberBox.Clear();
            AddressBox.Clear();
            BirthdayBox.SelectedDate = null;
            ColourBox.SelectedColor = System.Windows.Media.Colors.LightGray;
            NotesBox.Clear();
            SalaryBox.Clear();
        }

        private void ContactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Collapse the previous selected items
            foreach (var removedItem in e.RemovedItems)
            {
                if (removedItem is Person removedPerson)
                {
                    removedPerson.IsCardFocused = false;
                }
            }

            // Expand the newly selected item
            foreach (var addedItem in e.AddedItems)
            {
                if (addedItem is Person addedPerson)
                {
                    addedPerson.IsCardFocused = true;
                }
            }
        }
    }
}
