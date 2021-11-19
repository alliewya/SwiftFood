using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;
using System.Security.Cryptography;

namespace SwiftFood
{
    class SwiftFoodDatabase
    {
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

            // Find username in database


            // Hash password
            string hashedpassword = GetHashString(password);


            // Compare Hashes


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
    }
}
