using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Library_Management_System.Manager
{
    public class DatabaseManager
    {
        //// Use the method to get the connection string
        public static string conpath = ConfigurationManager.ConnectionStrings["ProductionConnection"].ConnectionString;
    }
}