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
        }
    

    }

}