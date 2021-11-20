using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using SQLite;

namespace SwiftFood
{

    public class User : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        private string username, password;

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }   
        
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        //Address Details
        private string address1,address2,postcode,email;

        public string Address1
        {
            get
            {
                return address1;
            }
            set
            {
                if (address1 != value)
                {
                    address1 = value;
                    OnPropertyChanged("Address1");
                }
            }
        }
        public string Address2
        {
            get
            {
                return address2;
            }
            set
            {
                if (address2 != value)
                {
                    address2 = value;
                    OnPropertyChanged("Address2");
                }
            }
        }
        public string Postcode
        {
            get
            {
                return postcode;
            }
            set
            {
                if (postcode != value)
                {
                    postcode = value;
                    OnPropertyChanged("Postcode");
                }
            }
        }
        
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        public User()
        {

        }

        public User(string postcode)
        {
            Postcode = postcode;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyname)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
