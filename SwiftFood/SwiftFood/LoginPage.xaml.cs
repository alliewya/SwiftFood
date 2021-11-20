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
    public partial class LoginPage : ContentPage
    {

        SwiftFoodDatabase STDB;

        public LoginPage()
        {
            InitializeComponent();

            STDB = new SwiftFoodDatabase();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            // Bypass Login to get to admin -> todo add admin user
            if(txtUsername.Text == "admin")
            {
                await Navigation.PushModalAsync(new NavigationPage(new AdminPage()));
            } else
            {
                //Check Login against database - database function sets global active user 
                if (STDB.ValidateUser(txtUsername.Text, txtPassword.Text))
                {
                    await Navigation.PushModalAsync(new MainPage());
                    VisualStateManager.GoToState(txtLoginWarning, "Normal");
                }
                else
                {
                    VisualStateManager.GoToState(txtLoginWarning, "Invalid");
                }
            }



        }

        private async void btnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRegistration());
        }
    }
}