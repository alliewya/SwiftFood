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
    public partial class FoodPage : ContentPage
    {
        App app = (App)Application.Current;
        string size = "Small";

        OrderItem CurrentOrderItem;

        public FoodPage(string name)
        {
            InitializeComponent();

            CurrentOrderItem = new OrderItem(app.ActiveFood, 1, "Medium",app.ActiveResturant.RestName);
            
            //Binding contexts
            txtItemTotal.BindingContext = CurrentOrderItem;
            txtQTY.BindingContext = CurrentOrderItem;
            RadioSize.BindingContext = CurrentOrderItem;

            //this.BindingContext = new OrderItem(app.ActiveFood, 1, "small");

            //Dynamic Text
            Foodname.Text = "Item to Order: " + name;
            smallprice.Text = CurrentOrderItem.PriceAtSize("Small").ToString();
            mediumprice.Text = CurrentOrderItem.PriceAtSize("Medium").ToString();
            largeprice.Text = CurrentOrderItem.PriceAtSize("Large").ToString();

        }
       
        private void Qtyplus_Clicked(object sender, EventArgs e)
        {
            int plus = Convert.ToInt32(txtQTY.Text);
            if (plus < 7)
            {
                plus++;
            }
            txtQTY.Text = Convert.ToString(plus);
            CurrentOrderItem.Qty = plus;
        }

        private void Qtyminus_Clicked(object sender, EventArgs e)
        {
            int plus = Convert.ToInt32(txtQTY.Text);
            if (plus > 1)
            {
                plus--;
            }
            txtQTY.Text = Convert.ToString(plus);
            CurrentOrderItem.Qty = plus;
        }

        private async void Addtobasket_Clicked(object sender, EventArgs e)
        {
            int QTy = Convert.ToInt32(txtQTY.Text);
            //OrderItem orderItem = new OrderItem(app.ActiveFood, QTy, size);
            //app.ActiveBasket.AddToOrder(app.ActiveFood, QTy, size);
            app.ActiveBasket.AddOrderItem(CurrentOrderItem);
            await Navigation.PopAsync();
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Calculate_Clicked(object sender, EventArgs e)
        { 
            int QTy = Convert.ToInt32(txtQTY.Text);
            OrderItem orderItem = new OrderItem(app.ActiveFood,QTy, size,app.ActiveResturant.RestName);
            string Itemprice = Convert.ToString(orderItem.ItemTotal);
            //Resetting the total Price so the user can see
            TotalPricetxt.Text = "Total Price: £";
            TotalPricetxt.Text = TotalPricetxt.Text + Itemprice;
        }

        private void Small_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
        
            size = "small";
            CurrentOrderItem.Size = "Small";
        }

        private void Medium_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
           
            size = "medium";
            CurrentOrderItem.Size = "Medium";
        }

        private void Large_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            size = "large";
            CurrentOrderItem.Size = "Large";
        }


    }
    }
