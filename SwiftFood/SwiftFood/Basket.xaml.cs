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
    public partial class Basket : ContentPage
    {

        App app = (App)Application.Current;


        public Basket()
        {
            InitializeComponent();
        }
    }
}