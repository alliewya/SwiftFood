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
    public partial class Checkout : ContentPage
    {

        App app = (App)Application.Current;

        UserViewModel UserVM;

        public Checkout()
        {
            InitializeComponent();

            //Current User ViewModel 
            UserVM = new UserViewModel();
            UserVM.Load(app.ActiveUser);
            this.BindingContext = UserVM;

            // Immediately view the total at the bottom of the screen
            decimal TotalPrice = app.ActiveBasket.OrderTotal;
            txtTotalPrice.Text = "£";
            txtTotalPrice.Text = txtTotalPrice.Text + TotalPrice;
        

        
        }




        private void btnApplyCode_Clicked(object sender, EventArgs e)
        {
            string discountCode = "ABC123";
            decimal Discount = app.ActiveBasket.Discount;
            decimal TotalPrice = app.ActiveBasket.OrderTotal;
            Discount = Math.Round((TotalPrice * 0.1m),2); //10% discount when code is entered

            if (txtDiscountCode.Text == discountCode)
            {
                txtDiscount.Text = "£" + Discount;
                txtDiscount.Text = "£" + Discount;
                TotalPrice = TotalPrice - Discount; //Updating total price and displaying
                txtTotalPrice.Text = "£";
                txtTotalPrice.Text = txtTotalPrice.Text + TotalPrice;

                lbldiscount.IsVisible = true;
                txtDiscount.IsVisible = true;
            }
            else
            {
                txtDiscount.Text = "£" + "0.00";
            }

        }

        private async void btnComplete_Clicked(object sender, EventArgs e)
        {
            SwiftFoodDatabase STDB = new SwiftFoodDatabase();
            STDB.SaveOrder(app.ActiveBasket);
            
            await Navigation.PushModalAsync(new Confirmation());
        }


        protected override void OnAppearing()
        {   //On page appearing
            base.OnAppearing();

            // Refresh local basket
            UserVM.Load(app.ActiveUser);

            if (app.ActiveBasket.OrderTotal == 0)
            {
                emptylayout.IsVisible = true;
                notemptylayout.IsVisible = false;
                btncheckout.IsEnabled = false;
            }
            else
            {
                emptylayout.IsVisible = false;
                notemptylayout.IsVisible = true;
                btncheckout.IsEnabled = true;
            }
        }
    }
}