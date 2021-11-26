using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;
using System.IO;
using System.Security.Cryptography;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwiftFood
{
    class SwiftFoodDatabase
    {
        App app = (App)Application.Current;

        public SQLiteConnection Database;
        public string DBStatus;

        public SwiftFoodDatabase()
        {
            try
            {
                Database = new SQLiteConnection(DBSetUp.DatabasePath, DBSetUp.Flags);

                // Users Table
                Database.CreateTable<User>();

                // Orders Table
                Database.CreateTable<Order>();

                // Order Items Table
                Database.CreateTable<OrderItem>();

                DBStatus = "DB Created Successfully";
            }
            catch (SQLiteException ex)
            {
                DBStatus = ex.Message;
            }

        }



        // Database Functions

        public bool ValidateUser(string username, string password)
            //Check user credentials are correct and if so return true bool
        {
            bool validated = false;

            //Hash password
            string hashedpassword = GetHashString(password);

            // Find username in database (todo -> rewrite sql to check for password also)
            List<User> tempusers = Database.Query<User>("SELECT * from User WHERE Username = ?", username); //TODO REMOVE CASE SENSITIVITY 
            Console.WriteLine(tempusers);
            Console.WriteLine(password);
            Console.WriteLine(hashedpassword);

            foreach(User x in tempusers)
            {
                Console.WriteLine(x.Password);
                if (x.Password == hashedpassword)
                {
                    app.ActiveUser = x;
                    validated = true;
                    break;
                }
            }

            return validated;
        }

        public int AddUser(User user)
        {
            User temp = user;
            //Hash password for storage
            temp.Password = GetHashString(user.Password);

            var savestatus = Database.Insert(temp);
            return savestatus;
        }

        public int SaveUser(User user)
        {
            int savestatus;

            if(user.Password.Length == 64)
            {
                savestatus = Database.Update(user);
            } else
            {
                User temp = user;
                //Hash password for storage
                temp.Password = GetHashString(user.Password);

                // Save to the DB
                savestatus = Database.Update(temp);
            }
            
            return savestatus;
        }

        public int DeleteUser(User user)
        {
            var savestatus = Database.Delete(user);
            return savestatus;
        }

        public List<User> GetUsers()
        {
            return Database.Table<User>().ToList();
        }



        //Hashing functions for password storage
        public static byte[] GetHash(string input)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        public static string GetHashString(string input)
            //Returns a SHA256 hashed string from an input
        {
            StringBuilder sb = new StringBuilder();
            byte[] hash = GetHash(input);
            foreach (byte x in hash)
            {
                sb.Append(x.ToString("X2"));
            }
            return sb.ToString();
        }


        // Order Database Functions


        public void SaveOrder(Order order)
        { //Save supplied order under the active username field. If username is blank (not logged in) order will be saved but not attributed to a user (intended)
            order.OrderUsername = app.ActiveUser.Username;
            order.OrderComplete = true;
            int savestatus = Database.Insert(order);
            Console.WriteLine(savestatus);
            Order temp = Database.Query<Order>("SELECT * FROM `Order` ORDER BY OrderID DESC LIMIT 1;")[0];
            // Attribute order id to order items and store in order items table
            foreach (OrderItem x in order.OrderItems)
            {
                x.OrderNumber = temp.OrderID;
                Database.Insert(x);
            }
        }

        public List<Order> GetOrders(string username)
        {
            //try:
            List<Order> results = Database.Query<Order>("SELECT * from `Order` WHERE OrderUsername = ?", username);

            return results;

            //return Database.Table<Order>().ToList();

        }

        public List<Order> GetAllOrders()
        {

            return Database.Table<Order>().ToList();

        }


        public List<OrderItem> GetOrderItems(int orderid)
        {
            List<OrderItem> results2 = Database.Query<OrderItem>("SELECT * from OrderItem WHERE OrderNumber = ?", orderid);
            return results2;

        }

        public Order GetOrderItemsAsOrder(int orderid)
        {
            List<OrderItem> results2 = Database.Query<OrderItem>("SELECT * from OrderItem WHERE OrderNumber = ?", orderid);
            ObservableCollection<OrderItem> temp = new ObservableCollection<OrderItem>(results2);
            Order result = new Order();
            result.OrderItems = temp;
            foreach(OrderItem x in result.OrderItems)
            {
                x.CalculateTotal();
            }
            result.UpdateTotal();
            return result;
        }




    }
}
