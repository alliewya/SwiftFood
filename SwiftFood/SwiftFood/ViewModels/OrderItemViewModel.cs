using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace SwiftFood
{
    class OrderItemViewModel : INotifyPropertyChanged
    {

        App app = (App)Application.Current;

        public OrderItemViewModel()
        {

        }

        public OrderItemViewModel(OrderItem source)
        {
            Load(source);
            SourceItem = source;
        }

        OrderItem SourceItem;

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
                    if (price <= 2)
                    {
                        price = 2.01m; //Ensure price is never 0!
                    }
                    OnPropertyChanged("Price");
                }
            }
        }


        private decimal itemtotal;

        public decimal ItemTotal
        {
            get
            {
                return itemtotal;
            }
            set
            {
                if (itemtotal != value)
                {
                    itemtotal = value;
                    OnPropertyChanged("ItemTotal");
                }
            }
        }

        private string size;

        public string Size
        {
            get
            { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    CalculateTotal();
                    OnPropertyChanged("Size");
                }

            }
        }


        private int qty;
        public int Qty
        {
            get
            { return qty; }
            set
            {
                if (qty != value)
                {
                    qty = value;
                    CalculateTotal();
                    OnPropertyChanged("Qty");
                }

            }
        }

        private string restaurantname;
        public string RestaurantName
        {
            get
            {
                return restaurantname;
            }
            set
            {
                if (restaurantname != value)
                {
                    restaurantname = value;
                    OnPropertyChanged("RestaurantName");
                }

            }
        }


        //Properties for order history
        public int OrderItemID { get; set; }

        public int OrderNumber;



        public void CalculateTotal()
        //Calculate the item/row total by adjusting unit price by size and multiplying by qty
        {
            switch (Size)
            {
                case "Small":
                    ItemTotal = Price * Qty;
                    break;
                case "Medium":
                    ItemTotal = (Price + 2) * Qty;
                    break;
                case "Large":
                    ItemTotal = (Price + 4) * Qty;
                    break;
            }
        }

        public decimal PriceAtSize(string size)
        {
            switch (size)
            {
                case "Small":
                    return (Price);
                case "Medium":
                    return (Price + 2);
                case "Large":
                    return (Price + 4);
                default:
                    return (Price);
            }
        }



        public void Load(OrderItem source)
        {
            Name = source.Name;
            Description = source.Description;
            Price = source.Price;
            ItemTotal = source.ItemTotal;
            Size = source.Size;
            Qty = source.Qty;
            RestaurantName = source.RestaurantName;
            OrderItemID = source.OrderItemID;
            OrderNumber = source.OrderNumber;
        }

        public void Save(ref OrderItem destination)
        {
            destination.Name = Name;
            destination.Description = Description;
            destination.Price = Price;
            destination.ItemTotal = ItemTotal;
            destination.Size = Size;
            destination.Qty = Qty;
            destination.RestaurantName = RestaurantName;
            destination.OrderItemID = OrderItemID;
            destination.OrderNumber = OrderNumber;
        }

        private OrderItem ReturnAsOrderItem()
        {
            OrderItem temp = new OrderItem(new Food(Name, Price, Description), Qty, Size, RestaurantName);
            temp.ItemTotal = ItemTotal;
            temp.Size = Size;
            temp.OrderItemID = OrderItemID;
            temp.OrderNumber = OrderNumber;

            return temp;
        }

        public void UpdateGlobalBasket()
        {   //Save the vm as order item in its location in the global basket
            ObservableCollection<OrderItem> templist = app.ActiveBasket.OrderItems;

            OrderItem temporderitem = ReturnAsOrderItem();

            int index = templist.IndexOf(SourceItem);

            if(index == -1)
            {
                return;
            }

            templist.RemoveAt(index);
            templist.Insert(index,temporderitem);
            app.ActiveBasket.OrderItems = templist;
            app.ActiveBasket.UpdateTotal();

        }


        public void RemoveFromGlobalBasket()
        {   //Save the vm as order item in its location in the global basket
            ObservableCollection<OrderItem> templist = app.ActiveBasket.OrderItems;

            int index = templist.IndexOf(SourceItem);

            if (index == -1)
            {
                return;
            }

            templist.RemoveAt(index);
            app.ActiveBasket.OrderItems = templist;
            app.ActiveBasket.UpdateTotal();

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
