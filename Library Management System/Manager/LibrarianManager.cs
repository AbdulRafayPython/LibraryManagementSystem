using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Library_Management_System.Models;

namespace Library_Management_System.Manager
{
    public class LibrarianManager
    {
        public List<Librarian> GetLibrarians()
        {
            List<Librarian> librarians = new List<Librarian>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "SELECT UserID, Username FROM UserLogin"; 
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Librarian librarian = new Librarian
                    {
                        IssuedByID = Convert.ToInt32(reader["UserID"]),
                        StaffName = reader["Username"].ToString() 
                    };
                    librarians.Add(librarian);
                }
            }
            return librarians;
        }

    }
}