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

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            userVM = new UserViewModel();
            userVM.Load(app.ActiveUser);
            this.BindingContext = userVM;
        }
    }
}