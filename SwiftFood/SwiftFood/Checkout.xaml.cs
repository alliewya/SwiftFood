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
        
        public Checkout()
        {
            InitializeComponent();

            // Immediately view the total at the bottom of the screen
            decimal TotalPrice = app.ActiveBasket.OrderTotal;
            txtTotalPrice.Text = "Total Price: £";
            txtTotalPrice.Text = txtTotalPrice.Text + TotalPrice;
        }

        private async void btnComplete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new Confirmation()));            
        }

        private void btnApplyCode_Clicked(object sender, EventArgs e)
        {
            string discountCode = "ABC123";
            double Discount = Convert.ToDouble(app.ActiveBasket.Discount);
            double TotalPrice = Convert.ToDouble(app.ActiveBasket.OrderTotal);
            Discount = TotalPrice * 0.1; //10% discount when code is entered

            if (txtDiscountCode.Text == discountCode)
            {
                txtDiscount.Text = "Discount: £" + Discount;
                TotalPrice = TotalPrice - Discount; //Updating total price and displaying
                txtTotalPrice.Text = "Total Price: £";
                txtTotalPrice.Text = txtTotalPrice.Text + TotalPrice;
            }
            else
            {
                txtDiscount.Text = "Discount: £" + "0.00";
            }
        }

        private async void btnComplete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Confirmation());
        }
    }
}