using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Linq;

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

        public int NumRests { get; set; }

        private decimal deliverycost;
        public decimal DeliveryCost { 
            get
            {
                return deliverycost;
            }
             set
            {
                if (deliverycost != value)
                {
                    deliverycost = value;
                    OnPropertyChanged("DeliveryCost");
                }
            }
        }

        private decimal vat;
        public decimal VAT
        {
            get
            {
                return vat;
            }
            set
            {
                if (vat != value)
                {
                    vat = value;
                    OnPropertyChanged("VAT");
                }
            }
        }


        public void Load(Order order)
        {
            OrderItems = order.OrderItems;
            OrderTotal = order.OrderTotal;
            Discount = order.Discount;
            DeliveryCost = order.DeliveryCost;
            VAT = order.VAT;
            NumRests = order.NumRests;
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
            order.DeliveryCost = DeliveryCost;
            order.VAT = VAT;
            order.NumRests = NumRests;
            order.ItemCount= ItemCount;
            order.OrderID= OrderID;
            order.OrderUsername= OrderUsername;
            order.OrderDateTime= OrderDateTime;
            order.OrderComplete= OrderComplete;
        }


        public void AddToOrder(Food food, int qty, string size, string restname)
        //Add supplied food to the order, creating an order item
        {
            OrderItem addition = new OrderItem(food, qty, size, restname);
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

            VAT = Math.Round((OrderTotal - (OrderTotal / 1.2m)), 2);
            OnPropertyChanged("VAT");
        }

        public void CalculateDelivery()
        //Calculate the delivery cost
        {
            //Find the number of restaurants ordered from
            List<string> restlist = new List<string>();
            foreach (OrderItem x in OrderItems)
            {
                restlist.Add(x.RestaurantName);
            }
            NumRests = restlist.Distinct().Count();
            DeliveryCost = NumRests * 1.99m;
            OnPropertyChanged("DeliveryCost");
            OnPropertyChanged("NumRests");
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
