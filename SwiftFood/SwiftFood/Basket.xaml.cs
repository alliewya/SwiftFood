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
    public partial class Basket : ContentPage
    {

        App app = (App)Application.Current;

        OrderViewModel currentbasketVM;

        public Basket()
        {
            InitializeComponent();

            currentbasketVM = new OrderViewModel(app.ActiveBasket);
            this.BindingContext = currentbasketVM;


        }


        //Event handle for selecting an order item from collection view
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //On selection change in collection, open a new resturant page using selected movie object
            if (((CollectionView)sender).SelectedItem != null)
            {
                OrderItem selected = e.CurrentSelection.FirstOrDefault() as OrderItem;
                await Navigation.PushAsync(new BasketEdit(selected));
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
        }

        private void btncheckout_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Checkout());
        }
    }
}