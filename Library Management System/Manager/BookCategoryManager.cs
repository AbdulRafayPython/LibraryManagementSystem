using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Library_Management_System.Manager
{
    public class BookCategoryManager
    {
        public void AddCategory(BookCategory bookCategory)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "INSERT INTO Category(CategoryName) VALUES(@CategoryName)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CategoryName", bookCategory.CategoryName);
                cmd.ExecuteNonQuery();
                con.Close();
            }   
        }

        public List<BookCategory> GetBookCategories()
        {
            List<BookCategory> list = new List<BookCategory>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "Select * from Category";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BookCategory obj = new BookCategory();
                    obj.CategoryID = Convert.ToInt32(dt.Rows[i]["CategoryID"]);
                    obj.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    list.Add(obj);
                }

                return list;
            }
        }

        public BookCategory GetBookCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                BookCategory obj = new BookCategory();
                con.Open();
                string query = "select * from Category where CategoryID= " + id + "";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    obj.CategoryID = Convert.ToInt32((dt.Rows[i][0]).ToString());
                    obj.CategoryName = dt.Rows[i][1].ToString();
                }
                con.Close();
                return obj;
            }
        }

        public void UpdateBookCategory(BookCategory obj)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "UPDATE Category SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CategoryName", obj.CategoryName);
                cmd.Parameters.AddWithValue("@CategoryID", obj.CategoryID);
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteBookCategory(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "DELETE FROM Category WHERE CategoryID=@CategoryID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@CategoryID", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}