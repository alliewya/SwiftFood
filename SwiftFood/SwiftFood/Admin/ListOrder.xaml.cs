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
    public partial class ListOrder : ContentPage
    {

        App app = (App)Application.Current;

        List<Order> OrderHistoryList;

        public ListOrder()
        {
            InitializeComponent();

            //Retrieve all users from DB and bind to list
            SwiftFoodDatabase STDB = new SwiftFoodDatabase();

            OrderHistoryList = STDB.GetAllOrders();
            ordersLV.ItemsSource = OrderHistoryList;
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var search = e.NewDate;
            var queriedorder = from Ord in OrderHistoryList where (Ord.OrderDateTime.Date == search.Date) select Ord;
            ordersLV.ItemsSource = queriedorder;
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            ordersLV.ItemsSource = OrderHistoryList;
        }
    }
}