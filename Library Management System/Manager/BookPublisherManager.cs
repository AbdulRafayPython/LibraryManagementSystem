using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Library_Management_System.Manager
{
    public class BookPublisherManager
    {
        public void AddPublisher(BookPublisher bookPublisher)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "INSERT INTO Publisher(PublisherName, PublicationLanguage, PublicationType) VALUES(@PublisherName, @PublicationLanguage, @PublicationType)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PublisherName", bookPublisher.PublisherName);
                cmd.Parameters.AddWithValue("@PublicationLanguage", bookPublisher.PublicationLanguage);
                cmd.Parameters.AddWithValue("@PublicationType", bookPublisher.PublicationType);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<BookPublisher> GetPublishers()
        {
            List<BookPublisher> list = new List<BookPublisher>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "Select * from Publisher";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    BookPublisher obj = new BookPublisher();
                    obj.PublisherID = Convert.ToInt32(dt.Rows[i]["PublisherID"]);
                    obj.PublisherName = dt.Rows[i]["PublisherName"].ToString();
                    obj.PublicationLanguage = dt.Rows[i]["PublicationLanguage"].ToString();
                    obj.PublicationType = dt.Rows[i]["PublicationType"].ToString();
                    list.Add(obj);
                }

                return list;
            }
        }

        public BookPublisher GetPublisher(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                BookPublisher obj = new BookPublisher();
                con.Open();
                string query = "select * from Publisher where PublisherID= " + id + "";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    obj.PublisherID = Convert.ToInt32((dt.Rows[i][0]).ToString());
                    obj.PublisherName = dt.Rows[i][1].ToString();
                    obj.PublicationLanguage = dt.Rows[i][2].ToString();
                    obj.PublicationType = dt.Rows[i][3].ToString();
                }
                con.Close();
                return obj;
            }
        }

        public void UpdatePublisher(BookPublisher obj)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "UPDATE Publisher SET PublisherName = @PublisherName, PublicationLanguage = @PublicationLanguage, PublicationType = @PublicationType WHERE PublisherID = @PublisherID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PublisherName", obj.PublisherName);
                cmd.Parameters.AddWithValue("@PublicationLanguage", obj.PublicationLanguage);
                cmd.Parameters.AddWithValue("@PublicationType", obj.PublicationType);
                cmd.Parameters.AddWithValue("@PublisherID", obj.PublisherID);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void DeletePublisher(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "DELETE FROM Publisher WHERE PublisherID=@PublisherID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@PublisherID", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}