using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;


// Importing Custom Fonts
[assembly: ExportFont("BentonSans Bold.otf", Alias = "BentonBold")]
[assembly: ExportFont("BentonSans Medium.otf", Alias = "BentonMedium")]
[assembly: ExportFont("BentonSans Regular.otf", Alias = "BentonRegular")]


namespace SwiftFood
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            GenerateTestRestaurants();


            //Create global basket
            ActiveBasket = new Order();


            MainPage = new NavigationPage(new PostCode());
     
        }

        public Order ActiveBasket;

        public ObservableCollection<Restaurant> MasterRestaurantList;

        public ObservableCollection<Restaurant> ActiveRestaurants;
        public Restaurant ActiveResturant { get; set; }
        public Food ActiveFood { get; set; }


        public Order ActiveOrder { get; set; }

        public User ActiveUser;


        private void GenerateTestRestaurants()
        {
            MasterRestaurantList = new ObservableCollection<Restaurant>();
            Restaurant pizza = new Restaurant();
            Restaurant burger = new Restaurant();
            Restaurant pasta = new Restaurant();

            pizza.RestName = "Pizza Allegro";
            pizza.RestRating = 5;
            pizza.RestAddress = "23 Fort Road, Townland";
            pizza.RestDescription = "Real Italian Pizza";
            pizza.RestImageSource = "pizza.jpg";
            pizza.RestOpeningHours = "1pm - 11pm";
            pizza.RestPhone = "015 5902 2123";
            pizza.RestPostcode = "MK18 IBF";
            pizza.Menu.Add(new Food("Four Cheese Pizza", (decimal)11.99, "Mozzarella, Gorgonzola, Fontina and Parmigiano. Pizza quattro formaggi Italian: (four cheese pizza) is a variety of pizza in Italian cuisine that is topped with a combination of four kinds of cheese, usually melted together, with (rossa, red) or without (bianca, white) tomato sauce."));
            pizza.Menu.Add(new Food("Pasta Parcel", (decimal)8.99, "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes"));

            burger.RestName = "Burger Joint";
            burger.RestRating = 4;
            burger.RestAddress = "23 Fort Lane, New Townshire";
            burger.RestDescription = "Juicy Burgers";
            burger.RestImageSource = "burger.jpg";
            burger.RestOpeningHours = "4pm - 12pm";
            burger.RestPhone = "015 5902 2144";
            burger.RestPostcode = "MK18 1EG";
            burger.Menu.Add(new Food("Cheese Burger", (decimal)3.99, "Cheese Burger with a good burger, cheese, lettuce, onion rings and with some burger sauce"));
            burger.Menu.Add(new Food("Chips", (decimal)0.99, "A good set of chips to go with your burger"));
          

            pasta.RestName = "Don Ravioli";
            pasta.RestRating = 2;
            pasta.RestAddress = "3 Milano Street, A River Dirty Street";
            pasta.RestDescription = "Revenge is a dish best served cold.";
            pasta.RestImageSource = "pasta.jpg";
            pasta.RestOpeningHours = "2pm - 10pm";
            pasta.RestPhone = "01559 022 178";
            pasta.RestPostcode = "MK18 1AD";
            pasta.Menu.Add(new Food("Ravioli", (decimal)9.99, "Perfect for catering the day of your daughters wedding"));
            pasta.Menu.Add(new Food("Spaghetti Carbonara", (decimal)8.99, "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes"));
            pasta.Menu.Add(new Food("Spaghetti Bologenese", (decimal)8.99, "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs"));
            pasta.Menu.Add(new Food("Lorem Ipus with Riccotta", (decimal)6.99, "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs"));

            MasterRestaurantList.Add(pizza);
            MasterRestaurantList.Add(burger);
            MasterRestaurantList.Add(pasta);

            ActiveRestaurants = MasterRestaurantList;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

