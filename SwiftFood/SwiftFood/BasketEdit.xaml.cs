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
    public partial class BasketEdit : ContentPage
    {
        App app = (App)Application.Current;
        OrderItemViewModel OrderItemVM;

        public BasketEdit()
        {
            InitializeComponent();
        }

        public BasketEdit(OrderItem source)
        {
            InitializeComponent();
            OrderItemVM = new OrderItemViewModel(source);

            this.BindingContext = OrderItemVM;

            Foodname.Text = OrderItemVM.Name;
            smallprice.Text = OrderItemVM.PriceAtSize("Small").ToString();
            mediumprice.Text = OrderItemVM.PriceAtSize("Medium").ToString();
            largeprice.Text = OrderItemVM.PriceAtSize("Large").ToString();
            //Stepper1.Value = Convert.ToInt32(txtQTY.Text);
        }


        private void Qtyplus_Clicked(object sender, EventArgs e)
        {
            int plus = Convert.ToInt32(txtQTY.Text);
            if (plus < 7)
            {
                plus++;
            }
            txtQTY.Text = Convert.ToString(plus);
            OrderItemVM.Qty = plus;
        }

        private void Qtyminus_Clicked(object sender, EventArgs e)
        {
            int plus = Convert.ToInt32(txtQTY.Text);
            if (plus > 1)
            {
                plus--;
            }
            txtQTY.Text = Convert.ToString(plus);
            OrderItemVM.Qty = plus;
        }

        private void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (e.NewValue > e.OldValue)
            {
                Qtyplus_Clicked(sender, e);
            }
            else
            {
                Qtyminus_Clicked(sender, e);
            }
        }

        private async void Updatebasket_Clicked(object sender, EventArgs e)
        {
            int QTy = Convert.ToInt32(txtQTY.Text);
            //OrderItem orderItem = new OrderItem(app.ActiveFood, QTy, size);
            //app.ActiveBasket.AddToOrder(app.ActiveFood, QTy, size);
            OrderItemVM.UpdateGlobalBasket();
            
            await Navigation.PopAsync();
        }

        private void Return_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnRemove_Clicked(object sender, EventArgs e)
        {
            OrderItemVM.RemoveFromGlobalBasket();
            Navigation.PopAsync();
        }
    }
}