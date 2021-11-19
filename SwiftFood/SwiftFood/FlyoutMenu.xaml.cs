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

            menuView.ItemsSource = flyoutPageItems;
        }
    
        public class FlyoutPageItem
        {
            public string Title { get; set; }
            public Type TargetType { get; set; }
        }


    }

}