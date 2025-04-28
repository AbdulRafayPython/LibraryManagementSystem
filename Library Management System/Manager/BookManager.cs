using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Library_Management_System.Manager
{
    public class BookManager
    {
        public void AddBook(Book book)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "INSERT INTO Book (ISBN, BookTitle, CategoryID, PublisherID, PublicationYear, BookEdition, CopiesTotal, CopiesAvailable) VALUES (@ISBN, @BookTitle, @CategoryID, @PublisherID, @PublicationYear, @BookEdition, @CopiesTotal, @CopiesAvailable)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                cmd.Parameters.AddWithValue("@BookTitle", book.BookTitle);
                cmd.Parameters.AddWithValue("@CategoryID", book.CategoryID);
                cmd.Parameters.AddWithValue("@PublisherID", book.PublisherID);
                cmd.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
                cmd.Parameters.AddWithValue("@BookEdition", book.BookEdition);
                cmd.Parameters.AddWithValue("@CopiesTotal", book.CopiesTotal);
                cmd.Parameters.AddWithValue("@CopiesAvailable", book.CopiesAvailable);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Book> GetBooks()
        {
            List<Book> list = new List<Book>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
                    SELECT b.BookID, b.ISBN, b.BookTitle, b.CategoryID, b.PublisherID, b.PublicationYear, b.BookEdition, b.CopiesTotal, b.CopiesAvailable, 
                    c.CategoryName, p.PublisherName 
                    FROM Book b
                    JOIN Category c ON b.CategoryID = c.CategoryID
                    JOIN Publisher p ON b.PublisherID = p.PublisherID";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Book book = new Book
                    {
                        BookID = Convert.ToInt32(reader["BookID"]),
                        ISBN = reader["ISBN"].ToString(),
                        BookTitle = reader["BookTitle"].ToString(),
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        PublisherID = Convert.ToInt32(reader["PublisherID"]),
                        PublicationYear = Convert.ToInt32(reader["PublicationYear"]),
                        BookEdition = reader["BookEdition"].ToString(),
                        CopiesTotal = Convert.ToInt32(reader["CopiesTotal"]),
                        CopiesAvailable = Convert.ToInt32(reader["CopiesAvailable"]),
                        Category = new BookCategory
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            CategoryName = reader["CategoryName"].ToString()
                        },

                        Publisher = new BookPublisher
                        {
                            PublisherID = Convert.ToInt32(reader["PublisherID"]),
                            PublisherName = reader["PublisherName"].ToString()
                        }
                        
                    };
                    list.Add(book);
                }
            }
            return list;
        }

        public Book GetBook(int id)
        {
            Book book = null;
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
                    SELECT b.BookID, b.ISBN, b.BookTitle, b.CategoryID, b.PublisherID, b.PublicationYear, b.BookEdition, b.CopiesTotal, b.CopiesAvailable, 
                    c.CategoryName, p.PublisherName 
                    FROM Book b
                    JOIN Category c ON b.CategoryID = c.CategoryID
                    JOIN Publisher p ON b.PublisherID = p.PublisherID
                    WHERE b.BookID = @BookID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BookID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    book = new Book
                    {
                        BookID = Convert.ToInt32(reader["BookID"]),
                        ISBN = reader["ISBN"].ToString(),
                        BookTitle = reader["BookTitle"].ToString(),
                        CategoryID = Convert.ToInt32(reader["CategoryID"]),
                        PublisherID = Convert.ToInt32(reader["PublisherID"]),
                        PublicationYear = Convert.ToInt32(reader["PublicationYear"]),
                        BookEdition = reader["BookEdition"].ToString(),
                        CopiesTotal = Convert.ToInt32(reader["CopiesTotal"]),
                        CopiesAvailable = Convert.ToInt32(reader["CopiesAvailable"]),
                        Category = new BookCategory
                        {
                            CategoryID = Convert.ToInt32(reader["CategoryID"]),
                            CategoryName = reader["CategoryName"].ToString()
                        },
                        Publisher= new BookPublisher
                        {
                            PublisherID = Convert.ToInt32(reader["PublisherID"]),
                            PublisherName = reader["PublisherName"].ToString()
                        }
                    };
                }
            }
            return book;
        }

        public void UpdateBook(Book book)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "UPDATE Book SET ISBN = @ISBN, BookTitle = @BookTitle, CategoryID = @CategoryID, PublisherID = @PublisherID, PublicationYear = @PublicationYear, BookEdition = @BookEdition, CopiesTotal = @CopiesTotal, CopiesAvailable = @CopiesAvailable WHERE BookID = @BookID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                cmd.Parameters.AddWithValue("@BookTitle", book.BookTitle);
                cmd.Parameters.AddWithValue("@CategoryID", book.CategoryID);
                cmd.Parameters.AddWithValue("@PublisherID", book.PublisherID);
                cmd.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
                cmd.Parameters.AddWithValue("@BookEdition", book.BookEdition);
                cmd.Parameters.AddWithValue("@CopiesTotal", book.CopiesTotal);
                cmd.Parameters.AddWithValue("@CopiesAvailable", book.CopiesAvailable);
                cmd.Parameters.AddWithValue("@BookID", book.BookID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBook(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "DELETE FROM Book WHERE BookID = @BookID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BookID", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}