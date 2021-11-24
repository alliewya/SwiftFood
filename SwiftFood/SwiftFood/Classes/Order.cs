using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SQLite;

namespace SwiftFood
{
    public class OrderItem : Food
    //A food item present in an order
    {
        
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
                if( size != value)
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
        [PrimaryKey, AutoIncrement]
        public int OrderItemID { get; set; } //Unique

        public int OrderNumber; //Corresponding order - can be blank until saving

        public OrderItem(Food food, int qty, string size, string restname) : base(food.Name, food.Price, food.Description)
        {
            Qty = qty;
            Size = size;
            RestaurantName = restname;
            CalculateTotal();

        }

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
                case "small":
                    return (Price);
                case "medium":
                    return (Price + 2);
                case "large":
                    return (Price + 4);
                default:
                    return (Price);
            }
        }

    }


    public class Order : INotifyPropertyChanged
    // Contains a list of OrderItems and holds a running total
    {
        //Add ignore for sqlite for collection - cannot be stored
        [Ignore]
        public ObservableCollection<OrderItem> OrderItems { get; set; }

        public decimal OrderTotal;

        public decimal Discount;

        public int ItemCount;

        [PrimaryKey, AutoIncrement]
        public int OrderID { get; set; }

        public string OrderUsername;

        public DateTime OrderDateTime;

        public bool OrderComplete; 

        public Order()
        {
            OrderItems = new ObservableCollection<OrderItem>();
            Discount = 0;
            ItemCount = 0;
            UpdateTotal();

            OrderDateTime = DateTime.Now;
            OrderComplete = false;
        }

        public void AddToOrder(Food food,int qty, string size,string restname) 
            //Add supplied food to the order, creating an order item
        {
            OrderItem addition = new OrderItem(food, qty, size,restname);
            OrderItems.Add(addition);
            UpdateTotal();
            OnPropertyChanged("OrderItems");
            OnPropertyChanged("ItemCount");
        }

        public void AddOrderItem(OrderItem item)
        {
            OrderItems.Add(item);
            UpdateTotal();
            OnPropertyChanged("OrderItems");
            OnPropertyChanged("ItemCount");
        }

        public void UpdateTotal()
            //Calculate the total of all items in basket and add/apply discount
        {
            OrderTotal = 0;
            ItemCount = 0;
            foreach (OrderItem x in OrderItems)
            {
                ItemCount += x.Qty;
                OrderTotal += x.ItemTotal;
            }

            OrderTotal -= Discount;
            OnPropertyChanged("OrderTotal");
            OnPropertyChanged("ItemCount");
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
