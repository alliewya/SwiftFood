using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SwiftFood
{
    public partial class MainPage : FlyoutPage
    {
        App app = (App)Application.Current;
        public FlyoutMenu flyoutmenu;

        public MainPage()
        {
            InitializeComponent();

            //Initialise and set flyout menu page and browse page as detail page
            flyoutmenu = new FlyoutMenu();
            Flyout = flyoutmenu;
            Detail = new NavigationPage(new Browse(""));



            //Subscribe to flyout menu checkout button
            flyoutmenu.menuView.SelectionChanged += OnItemSelected;
            flyoutmenu.btnCheckout.Clicked += btnCheckout_Clicked;
            flyoutmenu.btnlogout.Clicked += btnLogout_Clicked;
           
        }

        void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                //flyoutmenu.menuView.SelectedItem = null;
                IsPresented = false;
            }
        }

        public void btnCheckout_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Checkout());
            flyoutmenu.menuView.SelectedItem = null;
            IsPresented = false;
        }


        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            if (app.ActiveUser.Username == null)
            {
                Detail = new NavigationPage(new LoginPage());
                IsPresented = false;
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new PostCode()));
                app.ActiveUser = null;
            }
        }
    }

    public class FlyoutPageItem
    {
        public string Title { get; set; }
        public Type TargetType { get; set; }
    }
}
