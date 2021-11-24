using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;

namespace SwiftFood
{
    class OrderViewModel : INotifyPropertyChanged
    {

        public OrderViewModel() { 
        
        }

        public OrderViewModel(Order order)
        {
            Load(order);
        }


        public ObservableCollection<OrderItem> OrderItems { get; set; }

        private decimal ordertotal;
        public decimal OrderTotal {
            get
            {
                return ordertotal;
            }
            set
            {
                if (ordertotal != value)
                {
                    ordertotal = value;
                    OnPropertyChanged("OrderTotal");
                }
            }
        }


        private decimal discount;
        public decimal Discount
        {
            get
            {
                return discount;
            }
            set
            {
                if (discount != value)
                {
                    discount = value;
                    OnPropertyChanged("Discount");
                }
            }
        }



        private int itemcount;
        public int ItemCount
        {
            get
            {
                return itemcount;
            }
            set
            {
                if (itemcount != value)
                {
                    itemcount = value;
                    OnPropertyChanged("ItemCount");
                }
            }
        }

        public int OrderID { get; set; }

        public string OrderUsername;

        public DateTime OrderDateTime;

        public bool OrderComplete;


        public void Load(Order order)
        {
            OrderItems = order.OrderItems;
            OrderTotal = order.OrderTotal;
            Discount = order.Discount;
            ItemCount = order.ItemCount;
            OrderID = order.OrderID;
            OrderUsername = order.OrderUsername;
            OrderDateTime = order.OrderDateTime;
            OrderComplete = order.OrderComplete;
        }

        public void Save(ref Order order)
        {
            order.OrderItems = OrderItems;
            order.OrderTotal=OrderTotal;
            order.Discount = Discount;
            order.ItemCount= ItemCount;
            order.OrderID= OrderID;
            order.OrderUsername= OrderUsername;
            order.OrderDateTime= OrderDateTime;
            order.OrderComplete= OrderComplete;
        }


        public void AddToOrder(Food food, int qty, string size)
        //Add supplied food to the order, creating an order item
        {
            OrderItem addition = new OrderItem(food, qty, size);
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
