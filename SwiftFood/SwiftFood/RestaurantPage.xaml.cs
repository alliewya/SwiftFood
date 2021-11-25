using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace SwiftFood
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestaurantPage : ContentPage
    {
        App app = (App)Application.Current;

        OrderViewModel currentbasketVM;

        public RestaurantPage()
        {
            InitializeComponent();
            FoodCollection.ItemsSource = app.ActiveResturant.Menu;
            this.BindingContext = app.ActiveResturant;

            //Set a basket view model for the page to allow binding
            currentbasketVM = new OrderViewModel();
            currentbasketVM.Load(app.ActiveBasket);
            txtbasketitemqty.BindingContext= currentbasketVM;
            txtbaskettotal.BindingContext = currentbasketVM;
            if(currentbasketVM.ItemCount > 0)
            {
                basketbar.IsVisible = true;
            }

        }

        private async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //On selection change in collection, open a new resturant page using selected movie object
            if (((CollectionView)sender).SelectedItem != null)
            {
                Food current = e.CurrentSelection.FirstOrDefault() as Food;
                app.ActiveFood = current;
                await Navigation.PushAsync(new FoodPage(app.ActiveFood.Name));
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

            // Refresh local basket
            currentbasketVM.Load(app.ActiveBasket);
            if (currentbasketVM.ItemCount > 0)
            {
                basketbar.IsVisible = true;
            } else
            {
                basketbar.IsVisible = false;
            }
        }

        private async void btncheckout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Basket());
        }


        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string phonenumber = phNumber.Text; //get the number
            PhoneDialer.Open(phonenumber);
        }
    }
}