using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwiftFood
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderHistorySingle : ContentPage
    {
        App app = (App)Application.Current;

        OrderViewModel historicbasketVM;
        private int OrderID;


        public OrderHistorySingle(int id, DateTime orderdate)
        {
            InitializeComponent();

            dtorderdate.Date = orderdate.Date;
            OrderID = id;
            GetOrder();

            this.BindingContext = historicbasketVM;
        }

        private void GetOrder()
        {
            SwiftFoodDatabase STDB = new SwiftFoodDatabase();

            historicbasketVM = new OrderViewModel(STDB.GetOrderItemsAsOrder(OrderID));

        }
    }
}