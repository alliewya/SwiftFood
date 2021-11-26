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
    public partial class OrderHistory : ContentPage
    {
        App app = (App)Application.Current;

        List<Order> OrderHistoryList;

        public OrderHistory()
        {
            InitializeComponent();


            //gethistory(); //Runs on creation with the OnAppearing Function

        }

        private void gethistory()
        {
            if (app.ActiveUser.UserID == 0)
            {
                loggedinlayout.IsVisible = false;
                loggedoutlayout.IsVisible = true;
                OrderHistoryList = new List<Order>();
            }
            else
            {
                //Retrieve all users from DB and bind to list
                SwiftFoodDatabase STDB = new SwiftFoodDatabase();

                OrderHistoryList = STDB.GetOrders(app.ActiveUser.Username);
                ordersLV.ItemsSource = OrderHistoryList;
                swiftbucks();
            }
        }
        
        private void swiftbucks()
        {
            int bucks = 0;
            foreach (Order x in OrderHistoryList)
            {
                bucks += Convert.ToInt32((Math.Round(x.OrderTotal, 0) * 3));
            }
            txtSwiftbucks.Text = bucks.ToString();
        }

        private async void btnlogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void btnregister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRegistration());
        }

        private async void ordersLV_ItemSelected(object sender, SelectionChangedEventArgs e)
        {
            //On selection change in collection, open a new resturant page using selected movie object
            if (((CollectionView)sender).SelectedItem != null)
            {
                Order selected = e.CurrentSelection.FirstOrDefault() as Order;
                await Navigation.PushAsync(new OrderHistorySingle(selected.OrderID,selected.OrderDateTime));
                ((CollectionView)sender).SelectedItem = null;
            }
            else
            {
                return;
            }
        }


        protected override void OnAppearing()
        {   //On page appearing
            base.OnAppearing();

            // Refresh history
            gethistory();
        }
    }
}