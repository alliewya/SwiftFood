using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SwiftFood
{
    public class Food : INotifyPropertyChanged
    {
        //Individual menu items of food
        private string name;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string description;

        public string Description
        // Long text description of the food
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private decimal price;

        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                if (price != value)
                {
                    price = decimal.Round(value, 2);
                    OnPropertyChanged("Price");
                }
            }
        }
        public string ShowPrice => $"£ {Price}";


        public Food(string name, decimal price, string description = "No description entered")
        {
            Name = name;
            Price = price;
            Description = description;

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
