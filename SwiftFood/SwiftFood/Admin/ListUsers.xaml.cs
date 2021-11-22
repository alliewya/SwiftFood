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
    public partial class ListUsers : ContentPage
    {
        App app = (App)Application.Current;

        public ListUsers()
        {
            InitializeComponent();


            //Retrieve all users from DB and bind to list
            SwiftFoodDatabase STDB = new SwiftFoodDatabase();
            var allusers = STDB.GetUsers();
            usersLV.ItemsSource = allusers;


        }

        private async void usersLV_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                User newuser = e.SelectedItem as User;
                app.ActiveUser = newuser;

                await Navigation.PushAsync(new UserDetails(true));
                ((ListView)sender).SelectedItem = null;
            } else
            {
                return;
            }
        }

        protected override void OnAppearing()
        //Reloads the items when the page is opened - to update for changes in movies
        {
            base.OnAppearing();
            SwiftFoodDatabase STDB = new SwiftFoodDatabase();
            var allusers = STDB.GetUsers();
            usersLV.ItemsSource = allusers;
        }
    }
}