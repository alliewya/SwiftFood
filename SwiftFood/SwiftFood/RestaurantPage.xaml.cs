using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwiftFood
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestaurantPage : ContentPage
    {
        App app = (App)Application.Current;
        public RestaurantPage()
        {
            InitializeComponent();
            FoodCollection.ItemsSource = app.ActiveResturant.Menu;
            this.BindingContext = app.ActiveResturant;
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

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}