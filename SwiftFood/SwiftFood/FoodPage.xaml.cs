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
        string size = "small";
        public FoodPage(string name)
        {
            InitializeComponent();
            //this.BindingContext = new OrderItem(app.ActiveFood, 1, "small");
            Foodname.Text = "Item to Order: " + name;
        }
       
        private void Qtyplus_Clicked(object sender, EventArgs e)
        {
            int plus = Convert.ToInt32(txtQTY.Text);
            if (plus < 7)
            {
                plus++;
            }
            txtQTY.Text = Convert.ToString(plus);
        }

        private void Qtyminus_Clicked(object sender, EventArgs e)
        {
            int plus = Convert.ToInt32(txtQTY.Text);
            if (plus > 1)
            {
                plus--;
            }
            txtQTY.Text = Convert.ToString(plus);
        }

        private void Addtobasket_Clicked(object sender, EventArgs e)
        {
            int QTy = Convert.ToInt32(txtQTY.Text);
            //OrderItem orderItem = new OrderItem(app.ActiveFood, QTy, size);
            app.ActiveBasket.AddToOrder(app.ActiveFood, QTy, size);
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Calculate_Clicked(object sender, EventArgs e)
        { 
            int QTy = Convert.ToInt32(txtQTY.Text);
            OrderItem orderItem = new OrderItem(app.ActiveFood,QTy, size);
            string Itemprice = Convert.ToString(orderItem.ItemTotal);
            //Resetting the total Price so the user can see
            TotalPricetxt.Text = "Total Price: £";
            TotalPricetxt.Text = TotalPricetxt.Text + Itemprice;
        }

        private void Small_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
        
            size = "small";
        }

        private void Medium_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
           
            size = "medium";
        }

        private void Large_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            size = "large";
        }

        private void Calculate_Clicked_1(object sender, EventArgs e)
        {

        }
    }
    }
