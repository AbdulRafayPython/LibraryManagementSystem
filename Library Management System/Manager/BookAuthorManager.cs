using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Library_Management_System.Manager
{
    public class BookAuthorManager
    {
        public void AddBookAuthor(BookAuthor bookAuthor)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "INSERT INTO BookAuthor (BookID, AuthorID) VALUES (@BookID, @AuthorID)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BookID", bookAuthor.BookID);
                cmd.Parameters.AddWithValue("@AuthorID", bookAuthor.AuthorID);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<BookAuthor> GetBookAuthors()
        {
            List<BookAuthor> list = new List<BookAuthor>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
            SELECT ba.BookID, ba.AuthorID, b.BookTitle, a.AuthorName
            FROM BookAuthor ba
            JOIN Book b ON ba.BookID = b.BookID
            JOIN Author a ON ba.AuthorID = a.AuthorID";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    BookAuthor bookAuthor = new BookAuthor
                    {
                        BookID = Convert.ToInt32(row["BookID"]),
                        AuthorID = Convert.ToInt32(row["AuthorID"]),
                        Book = new Book
                        {
                            BookID = Convert.ToInt32(row["BookID"]),
                            BookTitle = row["BookTitle"].ToString()
                        },
                        Author = new Author
                        {
                            AuthorID = Convert.ToInt32(row["AuthorID"]),
                            AuthorName = row["AuthorName"].ToString()
                        }
                    };
                    list.Add(bookAuthor);
                }

                con.Close();
                return list;
            }
        }

        public BookAuthor GetBookAuthor(int bookID, int authorID)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                BookAuthor bookAuthor = null;
                con.Open();
                string query = @"
            SELECT ba.BookID, ba.AuthorID, b.BookTitle, a.AuthorName
            FROM BookAuthor ba
            JOIN Book b ON ba.BookID = b.BookID
            JOIN Author a ON ba.AuthorID = a.AuthorID
            WHERE ba.BookID = @BookID AND ba.AuthorID = @AuthorID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BookID", bookID);
                cmd.Parameters.AddWithValue("@AuthorID", authorID);
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    bookAuthor = new BookAuthor
                    {
                        BookID = Convert.ToInt32(row["BookID"]),
                        AuthorID = Convert.ToInt32(row["AuthorID"]),
                        Book = new Book
                        {
                            BookID = Convert.ToInt32(row["BookID"]),
                            BookTitle = row["BookTitle"].ToString()
                        },
                        Author = new Author
                        {
                            AuthorID = Convert.ToInt32(row["AuthorID"]),
                            AuthorName = row["AuthorName"].ToString()
                        }
                    };
                }

                con.Close();
                return bookAuthor;
            }
        }

        public void UpdateBookAuthor(BookAuthor bookAuthor)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "UPDATE BookAuthor SET AuthorID = @AuthorID WHERE BookID = @BookID AND AuthorID = @OldAuthorID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BookID", bookAuthor.BookID);
                cmd.Parameters.AddWithValue("@AuthorID", bookAuthor.AuthorID);
                cmd.Parameters.AddWithValue("@OldAuthorID", bookAuthor.AuthorID); 
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeleteBookAuthor(int bookID, int authorID)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "DELETE FROM BookAuthor WHERE BookID = @BookID AND AuthorID = @AuthorID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BookID", bookID);
                cmd.Parameters.AddWithValue("@AuthorID", authorID);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}