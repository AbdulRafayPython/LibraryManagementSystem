using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Library_Management_System.Manager
{
    public class BookReturnManager
    {
        public BookReturn bookReturn = new BookReturn();


        public void AddBookReturn(BookReturn bookReturn)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    string addReturnQuery = @"
                    INSERT INTO BookReturn (IssueID, ReturnDate, FineAmount)
                    VALUES (@IssueID, @ReturnDate, @FineAmount)";
                    SqlCommand addReturnCmd = new SqlCommand(addReturnQuery, con, transaction);
                    addReturnCmd.Parameters.AddWithValue("@IssueID", bookReturn.IssueID);
                    addReturnCmd.Parameters.AddWithValue("@ReturnDate", bookReturn.ReturnDate);
                    bookReturn.FineAmount = CalculateFine(bookReturn.IssueID, bookReturn.ReturnDate);
                    addReturnCmd.Parameters.AddWithValue("@FineAmount", bookReturn.FineAmount);
                    addReturnCmd.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }

        public void UpdateBookIssueReturnDate(int issueID, DateTime returnDate)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("UPDATE BookIssue SET ReturnDate = @ReturnDate WHERE IssueID = @IssueID", con))
                {
                    cmd.Parameters.AddWithValue("@IssueID", issueID);
                    cmd.Parameters.AddWithValue("@ReturnDate", returnDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public decimal CalculateFine(int issueID, DateTime returnDate)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();

                string getDueDateQuery = "SELECT DueDate FROM BookIssue WHERE IssueID = @IssueID";
                SqlCommand getDueDateCmd = new SqlCommand(getDueDateQuery, con);
                getDueDateCmd.Parameters.AddWithValue("@IssueID", issueID);

                DateTime dueDate = (DateTime)getDueDateCmd.ExecuteScalar();
                int overdueDays = Math.Max(0, (returnDate.Date - dueDate.Date).Days);
                decimal fineRatePerDay = 1m;
                decimal fineAmount = overdueDays * fineRatePerDay;

                return fineAmount;
            }
        }


        public List<BookReturn> GetBookReturns()
        {
            List<BookReturn> list = new List<BookReturn>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
                SELECT br.*, bi.MemID, m.FName, m.LName, b.BookID, b.BookTitle
                FROM BookReturn br
                INNER JOIN BookIssue bi ON br.IssueID = bi.IssueID
                INNER JOIN Member m ON bi.MemID = m.MemID
                INNER JOIN Book b ON bi.BookID = b.BookID";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    BookReturn bookReturn = new BookReturn
                    {
                        ReturnID = Convert.ToInt32(reader["ReturnID"]),
                        IssueID = Convert.ToInt32(reader["IssueID"]),
                        ReturnDate = Convert.ToDateTime(reader["ReturnDate"]),
                        FineAmount = Convert.ToDecimal(reader["FineAmount"]),
                        BookIssue = new BookIssue
                        {
                            IssueID = Convert.ToInt32(reader["IssueID"])
                        }
                    };
                    list.Add(bookReturn);
                }
            }
            return list;
        }

        public BookReturn GetBookReturn(int id)
        {
            BookReturn bookReturn = null;
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
                SELECT br.*, bi.MemID, m.FName, m.LName, b.BookID, b.BookTitle
                FROM BookReturn br
                INNER JOIN BookIssue bi ON br.IssueID = bi.IssueID
                INNER JOIN Member m ON bi.MemID = m.MemID
                INNER JOIN Book b ON bi.BookID = b.BookID
                WHERE br.ReturnID = @ReturnID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ReturnID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    bookReturn = new BookReturn
                    {
                        ReturnID = Convert.ToInt32(reader["ReturnID"]),
                        IssueID = Convert.ToInt32(reader["IssueID"]),
                        ReturnDate = Convert.ToDateTime(reader["ReturnDate"]),
                        FineAmount = Convert.ToDecimal(reader["FineAmount"]),
                        BookIssue = new BookIssue
                        {
                            IssueID = Convert.ToInt32(reader["IssueID"]),
                        }
                    };
                }
            }
            return bookReturn;
        }

        public void UpdateBookAvailability(int issueID, bool isReturn = true)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        string updateBookQuery = @"
          UPDATE Book
          SET CopiesAvailable = 
            CASE WHEN bi.IssueID IS NULL THEN b.CopiesTotal
                 ELSE 
                   CASE WHEN @isReturn = 1 THEN MIN(b.CopiesTotal, bi.CopiesAvailable + 1) 
                        ELSE MAX(0, bi.CopiesAvailable - 1) 
                   END
            END
          FROM Book b
          LEFT JOIN BookIssue bi ON b.BookID = bi.BookID
          WHERE bi.IssueID = @IssueID";

                        SqlCommand updateBookCmd = new SqlCommand(updateBookQuery, con, transaction);
                        updateBookCmd.Parameters.AddWithValue("@IssueID", issueID);
                        updateBookCmd.Parameters.AddWithValue("@isReturn", isReturn);
                        updateBookCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }


        public void DeleteBookReturn(int returnID)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();

                using (SqlTransaction transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Delete the Book Return record
                        string deleteReturnQuery = "DELETE FROM BookReturn WHERE ReturnID = @ReturnID";
                        SqlCommand deleteReturnCmd = new SqlCommand(deleteReturnQuery, con, transaction);
                        deleteReturnCmd.Parameters.AddWithValue("@ReturnID", returnID);
                        deleteReturnCmd.ExecuteNonQuery();

                        // Update Book availability (assuming deleted return is the latest)
                        string updateBookQuery = @"
                        UPDATE Book
                        SET CopiesAvailable = CopiesAvailable + 1
                        FROM Book b
                        INNER JOIN BookIssue bi ON b.BookID = bi.BookID
                        INNER JOIN BookReturn br ON bi.IssueID = br.IssueID
                        WHERE br.ReturnID = @ReturnID AND br.ReturnID = (
                            SELECT TOP 1 ReturnID
                            FROM BookReturn
                            WHERE IssueID = (SELECT IssueID FROM BookReturn WHERE ReturnID = @ReturnID)
                            ORDER BY ReturnDate DESC
                        )";
                        SqlCommand updateBookCmd = new SqlCommand(updateBookQuery, con, transaction);
                        updateBookCmd.Parameters.AddWithValue("@ReturnID", returnID);
                        updateBookCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}