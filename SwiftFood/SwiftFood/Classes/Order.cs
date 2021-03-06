using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SQLite;
using System.Linq;

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

        public int OrderNumber { get; set; } //Corresponding order - can be blank until saving

        public OrderItem(Food food, int qty, string size, string restname) : base(food.Name, food.Price, food.Description)
        {
            Qty = qty;
            Size = size;
            RestaurantName = restname;
            CalculateTotal();

        }

        public OrderItem() : base()
        {

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

    }


    public class Order : INotifyPropertyChanged
    // Contains a list of OrderItems and holds a running total
    {
        //Add ignore for sqlite for collection - cannot be stored
        [Ignore]
        public ObservableCollection<OrderItem> OrderItems { get; set; }

        public decimal OrderTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal DeliveryCost { get; set; }

        public decimal VAT { get; set; }

        public int ItemCount { get; set; }

        [PrimaryKey, AutoIncrement]
        public int OrderID { get; set; }

        public string OrderUsername { get; set; }

        public DateTime OrderDateTime { get; set; }

        public bool OrderComplete { get; set; }

        public int NumRests { get; set; }

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
            //Calculate the total of all items in basket and add/apply discount + add delivery price
        {
            OrderTotal = 0;
            ItemCount = 0;
            foreach (OrderItem x in OrderItems)
            {
                ItemCount += x.Qty;
                OrderTotal += x.ItemTotal;
            }

            CalculateDelivery();
            OrderTotal += DeliveryCost;

            OrderTotal -= Discount;
            OnPropertyChanged("OrderTotal");
            OnPropertyChanged("ItemCount");

            VAT = Math.Round((OrderTotal - (OrderTotal / 1.2m)),2);
            OnPropertyChanged("VAT");
        }

        public void CalculateDelivery()
        //Calculate the delivery cost
        {
            //Find the number of restaurants ordered from
            List<string> restlist = new List<string>();
            foreach(OrderItem x in OrderItems)
            {
                restlist.Add(x.RestaurantName); 
            }
            NumRests = restlist.Distinct().Count();
            DeliveryCost = NumRests * 1.99m;
            OnPropertyChanged("DeliveryCost");
            OnPropertyChanged("NumRests");
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
