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
    public partial class Confirmation : ContentPage
    {
        App app = (App)Application.Current;

        public Confirmation()
        {
            InitializeComponent();

            //Reset current order
            app.ActiveBasket = new Order();
        }

        private async void btn_Return2Browse_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new MainPage());
        }


    }
}