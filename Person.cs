using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2 {
    class Person {
        private int _id;
        private string _firstName;
        private string _lastName;
        private int _salary;
        private string _phoneNumber;
        private string _email;
        private string _address;
        private DateOnly _birthday;
        private Color _color;
        private String _notes;

        public int ID {
            get { return _id; }
            set { _id = value; }
        }
        public string FirstName {
            get { return _firstName; }
            set {
                if(this._firstName != value)
                    _firstName = value;
            }
        }
        public string LastName {
            get { return _lastName; }
            set {
                if(this._lastName != value)
                    _lastName = value;
            }
        }
        public int Salary {
            get { return _salary; }
            set {
                if(this._salary != value)
                    _salary = value;
            }
        }
        public string PhoneNumber {
            get { return _phoneNumber; }
            set {
                if(this._phoneNumber != value)
                    _phoneNumber = value;
            }
        }
        public string Email {
            get { return _email; }
            set {
                if(this._email != value)
                    _email = value;
            }
        }
        public string Address {
            get { return _address; }
            set {
                if(this._address != value)
                    _address = value;
            }
        }
        public DateOnly Birthday {
            get { return _birthday; }
            set {
                if(this._birthday != value)
                    _birthday = value;
            }
        }
        public Color Color {
            get { return _color; }
            set {
                if(this._color != value)
                    _color = value;
            }
        }
        public String Notes {
            get { return _notes; }
            set {
                if(this._notes != value)
                    _notes = value;
            }
        }

        public Person() {
            this._id = 0;
            this._firstName = string.Empty;
            this._lastName = string.Empty;
            this._salary = 0;
            this._phoneNumber = string.Empty;
            this._email = string.Empty;
            this._address = string.Empty;
            this._birthday = DateOnly.MinValue;
            this._color = Color.LightGray;
            this._notes = string.Empty;
        }
    }
}