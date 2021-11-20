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
        {
            InitializeComponent();

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

        private async void btnSaveUser_Clicked(object sender, EventArgs e)
        {
            User activeuser = app.ActiveUser;
            userVM.Save(ref activeuser);
            userVM.SaveToDB(activeuser);

            await Navigation.PopAsync();
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            userVM = new UserViewModel();
            userVM.Load(app.ActiveUser);
            this.BindingContext = userVM;
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
    }
}