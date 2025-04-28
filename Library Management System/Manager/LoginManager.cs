using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Library_Management_System.Manager
{
    public class LoginManager
    {
        public bool ValidateLogin(string Username, string password)
        {
            bool loginSuccessful = false;

            string connectionString = DatabaseManager.conpath;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM UserLogin WHERE Username = @Username AND Password = @password";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", Username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        loginSuccessful = true;
                    }
                }
            }

            return loginSuccessful;
        }

        public List<Login> GetUsers()
        {
            List<Login> list= new List<Login>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "SELECT UserID, Username FROM UserLogin";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Login obj= new Login
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        Username = reader["Username"].ToString()
                    };
                    list.Add(obj);
                }
            }
            return list;
        }
        

    }


    


}