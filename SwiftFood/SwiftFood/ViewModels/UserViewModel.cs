using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;

namespace SwiftFood
{
    public class UserViewModel : INotifyPropertyChanged
    {

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
        private string address1, address2, postcode,email;

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

        public void Load(User user)
        {
            UserID = user.UserID;
            Username = user.Username;
            Password = user.Password;
            Address1 = user.Address1;
            Address2 = user.Address2;
            Postcode = user.Postcode;
            Email = user.Email;
        }

        public void Save(ref User user)
        {
            user.UserID = UserID;
            user.Username = Username;
            user.Password = Password;
            user.Address1 = Address1;
            user.Address2 = Address2;
            user.Postcode = Postcode;
            user.Email = Email;
        }

        public void SaveToDB(User user)
        {
            SwiftFoodDatabase STDB = new SwiftFoodDatabase();
            STDB.SaveUser(user);
        }

        public void SaveNewDB(User user)
        {
            SwiftFoodDatabase STDB = new SwiftFoodDatabase();
            STDB.AddUser(user);
        }

        public void DeleteUser(User user)
        {
            SwiftFoodDatabase STDB = new SwiftFoodDatabase();
            STDB.DeleteUser(user);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string PropertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

    }
}
