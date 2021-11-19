using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;

namespace SwiftFood
{
    public class DBSetUp
    {
        //Define the DB Name
        public const string DatabaseFileName = "SwiftFoodDatabase.db3";

        // Set the DB Settings/Flags
        public const SQLite.SQLiteOpenFlags Flags =
            SQLite.SQLiteOpenFlags.ReadWrite | //read write
            SQLite.SQLiteOpenFlags.Create; //Able create db


        public static string DatabasePath
        {
            get
            {
                //return the path
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFileName);
            }
        }
    }
}
