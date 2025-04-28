using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Library_Management_System.Manager
{
    public class AuthorManager
    {
        public void AddAuthor(Author author)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "INSERT INTO Author (AuthorName) VALUES (@AuthorName)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AuthorName", author.AuthorName);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<Author> GetAuthors()
        {
            List<Author> list = new List<Author>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "SELECT * FROM Author";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Author author = new Author();
                    author.AuthorID = Convert.ToInt32(dt.Rows[i]["AuthorID"]);
                    author.AuthorName = dt.Rows[i]["AuthorName"].ToString();
                    list.Add(author);
                }

                con.Close();
                return list;
            }
        }

        public Author GetAuthor(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                Author author = new Author();
                con.Open();
                string query = "SELECT * FROM Author WHERE AuthorID = @AuthorID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AuthorID", id);
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    author.AuthorID = Convert.ToInt32(dt.Rows[0]["AuthorID"]);
                    author.AuthorName = dt.Rows[0]["AuthorName"].ToString();
                }

                con.Close();
                return author;
            }
        }

        public void UpdateAuthor(Author author)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "UPDATE Author SET AuthorName = @AuthorName WHERE AuthorID = @AuthorID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AuthorName", author.AuthorName);
                cmd.Parameters.AddWithValue("@AuthorID", author.AuthorID);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteAuthor(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "DELETE FROM Author WHERE AuthorID = @AuthorID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AuthorID", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}