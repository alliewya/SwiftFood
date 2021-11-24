using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwiftFood
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostCode : ContentPage
    {
        App app = (App)Application.Current;

        public PostCode()
        {
           
            InitializeComponent();
            Logo.Source = "swiftfoodsmall.png";
            
        }

        private async void Find_Clicked(object sender, EventArgs e)
        {
            string postcode = PC.Text;
            if (ValidatePostcode())
            {

                //Set current user to new empty 'temp' user
                app.ActiveUser = new User(postcode);  //await Navigation.PushModalAsync(new Browse(PC.Text));

                await Navigation.PushModalAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Error", "Please enter a valid postcode ", "OK");
            }
        }
        public ICommand GotoLogin => new Command(Gotopage);
        private async void Gotopage()
        {
            await Navigation.PushAsync(new LoginPage());

        }

        private void PC_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PC.Text.Length > 5)
            {
                string state = "Valid";
                bool check = ValidatePostcode();
                if(check != true)
                {
                    state = "Invalid";
                } 
                VisualStateManager.GoToState(OuterStack, state);
            }
        }

        private bool ValidatePostcode()
        {
            bool isValid = false;

            //var regex = "^[A - Z]{ 1,2}[0 - 9][A-Z0 - 9]? ?[0 - 9][A-Z]{2}$";
            var regex = "^(([A-Z]{1,2}[0-9][A-Z0-9]?|ASCN|STHL|TDCU|BBND|[BFS]IQQ|PCRN|TKCA) ?[0-9][A-Z]{2}|BFPO ?[0-9]{1,4}|(KY[0-9]|MSR|VG|AI)[ -]?[0-9]{4}|[A-Z]{2} ?[0-9]{2}|GE ?CX|GIR ?0A{2}|SAN ?TA1)$";

            Match match = Regex.Match(PC.Text, regex, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                isValid = true;
            } 

            return isValid;
        }

    }
}