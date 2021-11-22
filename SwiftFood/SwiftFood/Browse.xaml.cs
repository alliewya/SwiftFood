using System;
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
            InitializeComponent();

            var restaurants = app.ActiveRestaurants;
            RestaurantCollection.ItemsSource = restaurants;

            txtPostcode.Text = app.ActiveUser.Postcode;

        }

        //Event handle for selecting a rsturant from collection view
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //On selection change in collection, open a new restaurant page using selected movie object
            if (((CollectionView)sender).SelectedItem != null)
           {
                Restaurant current = e.CurrentSelection.FirstOrDefault() as Restaurant;

                await Navigation.PushAsync(new Checkout());
                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}