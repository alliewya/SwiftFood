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

        public decimal ItemTotal;

        public string Size;

        public int Qty;


        //Properties for order history
        [PrimaryKey, AutoIncrement]
        public int OrderItemID { get; set; } //Unique

        public int OrderNumber; //Corresponding order - can be blank until saving

        public OrderItem(Food food, int qty, string size) : base(food.Name, food.Price, food.Description)
        {
            Qty = qty;
            Size = size;
            CalculateTotal();

        }

        public void CalculateTotal()
        //Calculate the item/row total by adjusting unit price by size and multiplying by qty
        {
            switch (Size)
            {
                case "small":
                    ItemTotal = (Price - 2) * Qty;
                    break;
                case "medium":
                    ItemTotal = Price * Qty;
                    break;
                case "large":
                    ItemTotal = (Price + 2) * Qty;
                    break;
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

        [PrimaryKey, AutoIncrement]
        public int OrderID { get; set; }

        public string OrderUsername;

        public DateTime OrderDateTime;

        public bool OrderComplete; 

        public Order()
        {
            OrderItems = new ObservableCollection<OrderItem>();
            Discount = 0;
            UpdateTotal();

            OrderDateTime = DateTime.Now;
            OrderComplete = false;
        }

        public void AddToOrder(Food food,int qty, string size) 
            //Add supplied food to the order, creating an order item
        {
            OrderItem addition = new OrderItem(food, qty, size);
            OrderItems.Add(addition);
            UpdateTotal();
            OnPropertyChanged("OrderItems");
        }

        public void UpdateTotal()
            //Calculate the total of all items in basket and add/apply discount
        {
            OrderTotal = 0;
            foreach(OrderItem x in OrderItems)
            {
                OrderTotal += x.ItemTotal;
            }

            OrderTotal -= Discount;
            OnPropertyChanged("OrderTotal");
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
