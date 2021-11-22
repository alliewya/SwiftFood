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
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private async void btnViewUsers_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListUsers());
        }

        private async void btnAddUser_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRegistration(true));
        }

        private async void btnLogout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PostCode()));
        }
    }
}