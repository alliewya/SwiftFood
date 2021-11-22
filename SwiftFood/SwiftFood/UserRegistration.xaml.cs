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
    public partial class UserRegistration : ContentPage
    {

        App app = (App)Application.Current;

        UserViewModel userVM;

        User newuser;

        bool Admin;

        public UserRegistration(bool admin=false)
        {
            InitializeComponent();

            Admin = admin;

            //View Model
            newuser = new User();
            userVM = new UserViewModel();
            userVM.Load(newuser);
            this.BindingContext = userVM;
        }

        private async void btnSaveUser_Clicked(object sender, EventArgs e)
        {
            //User activeuser = app.ActiveUser;
            userVM.Save(ref newuser);
            userVM.SaveNewDB(newuser);
            app.ActiveUser = newuser;

            if (Admin)
            {
                await Navigation.PopAsync();
            } else
            {
                await Navigation.PushModalAsync(new MainPage());
            }
            
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }


        private void txtpassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidatePassword();
        }

        private bool ValidatePassword()
        {
            bool isValid = true;
            string state = "Normal";
            // Check the number of characters in the username entry field
            var password = entryPassword.Text;

            //get number of characters
            int num_xters = password.Length;

            if (num_xters < 6)
            {
                isValid = false;
                state = "Invalid";

            }
            // Trigger message state change
            VisualStateManager.GoToState(entryPassword, state);
            VisualStateManager.GoToState(txtpasswordmsg, state);

            return isValid;
        }

    }
}