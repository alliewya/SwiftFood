﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwiftFood
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Browse : ContentPage
    {

        App app = (App)Application.Current;

        public Browse()
        {
            string postcode = app.ActiveUser.Postcode;

            InitializeComponent();
            //NearPostcode.Text = "Restaurants Near " + postcode + ":";
            NearPostcode.Text = postcode;
            var restaurants = app.ActiveRestaurants;
            RestaurantCollection.ItemsSource = restaurants;


        }

        //Event handle for selecting a rsturant from collection view
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //On selection change in collection, open a new resturant page using selected movie object
            if (((CollectionView)sender).SelectedItem != null)
            {
                Restaurant current = e.CurrentSelection.FirstOrDefault() as Restaurant;
                app.ActiveResturant = current;
                await Navigation.PushAsync(new RestaurantPage());
                ((CollectionView)sender).SelectedItem = null;
            }
            else
            {
                return;
            }
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var search = e.NewTextValue;
            var queriedresturant = from Rest in app.ActiveRestaurants where (Rest.RestName.ToLower().Contains(search.ToLower())) select Rest;
            RestaurantCollection.ItemsSource = queriedresturant;
           
        }

        private void OnCollectionViewScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            //Make the scroll for more prompt disappear when scrolling down

            //txtScroll.Text = e.VerticalOffset.ToString() + " " + RestaurantCollection.Height.ToString();
            scrollprompt.Opacity = (1 - (e.VerticalOffset / (RestaurantCollection.Height-76)));
        }
    }
}