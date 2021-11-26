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
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private async void btnViewUsers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListUsers());
        }

        private async void btnAddUser_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRegistration(true));
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PostCode()));
        }

        private async void btnOrders_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListOrder());
        }


        //private void salestoday()
        //{
        //    var search = new DateTime();
        //    search = DateTime.Now;

        //    SwiftFoodDatabase STDB = new SwiftFoodDatabase();

        //    var OrderHistoryList = STDB.GetAllOrders();

        //    var queriedorder = from Ord in OrderHistoryList where (Ord.OrderDateTime.Date == search.Date) select Ord;

        //    decimal sales = 0;

        //    foreach (Order x in queriedorder)
        //    {
        //        sales += x.OrderTotal;
        //    }

        //    saleslabel.Text = sales.ToString();

        //}

    }
}