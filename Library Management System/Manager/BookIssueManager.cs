using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Library_Management_System.Manager
{
    public class BookIssueManager
    {
        public void CreateBookIssue(BookIssue bookIssue)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
                {
                    con.Open();
                    string query = @"
                    INSERT INTO BookIssue (BookID, MemID, IssueDate, DueDate, ReturnDate, IssuedByID)
                    VALUES (@BookID, @MemID, @IssueDate, @DueDate, @ReturnDate, @IssuedByID)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@BookID", bookIssue.BookID);
                    cmd.Parameters.AddWithValue("@MemID", bookIssue.MemID);
                    cmd.Parameters.AddWithValue("@IssueDate", bookIssue.IssueDate);
                    cmd.Parameters.AddWithValue("@DueDate", bookIssue.DueDate);
                    cmd.Parameters.AddWithValue("@ReturnDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@IssuedByID", bookIssue.IssuedByID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error creating book issue: " + ex.Message);
            }
        }

        public List<BookIssue> GetBookIssues()
        {
            List<BookIssue> bookIssues = new List<BookIssue>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
        SELECT bi.*, b.BookTitle AS BookTitle, CONCAT(m.FName, ' ', m.LName) AS FullMemberName, ul.UserID AS IssuedByID, ul.Username AS IssuedBy
        FROM BookIssue bi
        INNER JOIN Book b ON bi.BookID = b.BookID
        INNER JOIN Member m ON bi.MemID = m.MemID
        INNER JOIN UserLogin ul ON bi.IssuedByID = ul.UserID";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BookIssue bookIssue = new BookIssue
                    {
                        IssueID = Convert.ToInt32(reader["IssueID"]),
                        BookID = Convert.ToInt32(reader["BookID"]),
                        MemID = Convert.ToInt32(reader["MemID"]),
                        IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                        DueDate = Convert.ToDateTime(reader["DueDate"]),
                        ReturnDate = reader["ReturnDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ReturnDate"]) : null,
                        IssuedByID = Convert.ToInt32(reader["IssuedByID"]),
                        FullMemberName = reader["FullMemberName"].ToString(),
                        Book = new Book
                        {
                            BookTitle = reader["BookTitle"].ToString()
                        },
                        Member = new Member
                        {
                            MemID = Convert.ToInt32(reader["MemID"])
                        },
                        IssuedBy = new Login
                        {
                            UserID = Convert.ToInt32(reader["IssuedByID"]),
                            Username = reader["IssuedBy"].ToString()
                        }
                    };
                    bookIssues.Add(bookIssue);
                }
            }
            return bookIssues;
        }




        public BookIssue GetBookIssue(int issueID)
        {
            BookIssue bookIssue = null;
            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseManager.conpath)) 
                {
                    con.Open();
                    string query = @"
                SELECT bi.*, b.BookTitle, CONCAT(m.FirstName, ' ', m.LastName) AS FullMemberName, ul.Username AS IssuedBy
                FROM BookIssue bi
                INNER JOIN Book b ON bi.BookID = b.BookID
                INNER JOIN Member m ON bi.MemID = m.MemID
                INNER JOIN UserLogin ul ON bi.IssuedByID = ul.UserID
                WHERE bi.IssueID = @IssueID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IssueID", issueID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        bookIssue = new BookIssue
                        {
                            IssueID = Convert.ToInt32(reader["IssueID"]),
                            BookID = Convert.ToInt32(reader["BookID"]),
                            MemID = Convert.ToInt32(reader["MemID"]),
                            IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                            DueDate = Convert.ToDateTime(reader["DueDate"]),
                            ReturnDate = reader["ReturnDate"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["ReturnDate"]) : null,
                            IssuedByID = Convert.ToInt32(reader["IssuedByID"]),
                            FullMemberName = reader["FullMemberName"].ToString(),
    
                            Book = new Book { BookTitle = reader["BookTitle"].ToString() },
                            Member = new Member 
                            {
                                MemID = Convert.ToInt32(reader["MemID"])
                            },
                            IssuedBy = new Login { UserID = Convert.ToInt32(reader["UserID"]), Username = reader["Username"].ToString() }
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error getting book issue: " + ex.Message);
            }
            return bookIssue;
        }


        public void UpdateBookIssue(BookIssue bookIssue)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
                {
                    con.Open();
                    string query = @"
                    UPDATE BookIssue
                    SET BookID = @BookID, MemID = @MemID, IssueDate = @IssueDate, DueDate = @DueDate, ReturnDate = @ReturnDate, IssuedByID = @IssuedByID
                    WHERE IssueID = @IssueID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IssueID", bookIssue.IssueID);
                    cmd.Parameters.AddWithValue("@BookID", bookIssue.BookID);
                    cmd.Parameters.AddWithValue("@MemID", bookIssue.MemID);
                    cmd.Parameters.AddWithValue("@IssueDate", bookIssue.IssueDate);
                    cmd.Parameters.AddWithValue("@DueDate", bookIssue.DueDate);
                    cmd.Parameters.AddWithValue("@ReturnDate", bookIssue.ReturnDate);
                    cmd.Parameters.AddWithValue("@IssuedByID", bookIssue.IssuedByID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error updating book issue: " + ex.Message);
            }
        }

        public void DeleteBookIssue(int issueID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
                {
                    con.Open();
                    string query = "DELETE FROM BookIssue WHERE IssueID = @IssueID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@IssueID", issueID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error deleting book issue: " + ex.Message);
            }
        }
    }
}