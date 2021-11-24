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
    public partial class UserDetails : ContentPage
    {
        App app = (App)Application.Current;

        UserViewModel userVM;

        bool Admin;

        public UserDetails(bool admin=false)
        {   //User details coming from admin page - additional non user facing functionality
            InitializeComponent();

            // Show login/register prompt page is no user is logged in
            if(app.ActiveUser.UserID == 0)
            {
                loggedinlayout.IsVisible = false;
                loggedoutlayout.IsVisible = true;
            }

            //Enable admin functions
            Admin = admin;
            if (Admin)
            {
                entryPassword.IsEnabled = true;
                btnDelete.IsVisible = true;
            }

            //View Model
            userVM = new UserViewModel();
            userVM.Load(app.ActiveUser);
            this.BindingContext = userVM;

        }

        public UserDetails()
        {
            InitializeComponent();

            Admin = false;
            // Show login/register prompt page is no user is logged in
            if (app.ActiveUser.UserID == 0)
            {
                loggedinlayout.IsVisible = false;
                loggedoutlayout.IsVisible = true;
            }

            //View Model
            userVM = new UserViewModel();
            userVM.Load(app.ActiveUser);
            this.BindingContext = userVM;

        }

        private async void btnSaveUser_Clicked(object sender, EventArgs e)
        {
            User activeuser = app.ActiveUser;
            userVM.Save(ref activeuser);
            userVM.SaveToDB(activeuser);

            if (Admin) 
            { await Navigation.PopAsync(); 
            } else
            {
                bool reply = await DisplayAlert("Success", "Your details have been updated successfully!", "Return to homepage", "Make additional changes");
                if (reply)
                {
                    await Navigation.PushModalAsync(new MainPage());
                }

            }
            
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            bool reply = await DisplayAlert("Warning", "Revert changes back to saved? You will lose and changes you have made", "Discard Changes", "Keep Changes");
            if (reply)
            {
                userVM = new UserViewModel();
                userVM.Load(app.ActiveUser);
                this.BindingContext = userVM;
            }
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning", "This delete action cannot be undone!", "OK", "Cancel");
            if (answer)
            {
                User temp = new User();
                userVM.Save(ref temp);
                userVM.DeleteUser(temp);
                
                await Navigation.PopAsync();
                await DisplayAlert("Success", "User Deleted", "OK");
            }

        }

        private async void btnlogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void btnregister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserRegistration());
        }
    }
}