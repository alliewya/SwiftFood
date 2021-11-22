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
    public partial class FlyoutMenu : ContentPage
    {
        App app = (App)Application.Current;

        public FlyoutMenu()
        {
            InitializeComponent();

            var flyoutPageItems = new List<FlyoutPageItem>();
            flyoutPageItems.Add(new FlyoutPageItem
            {
                Title = "Browse",
                TargetType = typeof(Browse)
            });
            flyoutPageItems.Add(new FlyoutPageItem
            {
                Title = "Checkout",
                TargetType = typeof(Checkout)
            });

            menuView.ItemsSource = flyoutPageItems;


            //Set button to logout if user - login if no user
            if(app.ActiveUser.Username == null)
            {
                btnlogout.Text = "Log In";
            } 
        }


    }

}