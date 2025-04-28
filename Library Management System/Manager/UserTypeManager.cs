using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI.WebControls;
using Library_Management_System.Models;

namespace Library_Management_System.Manager
{
    public class UserTypeManager
    {
        public void AddUserType(UserType userType)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "INSERT INTO MemberType (MemTypeName) VALUES (@MemTypeName)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MemTypeName", userType.UserTypeName);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public List<UserType> GetUserTypes()
        {
            List<UserType> list= new List<UserType>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "Select * from MemberType";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    UserType obj = new UserType();
                    obj.UserTypeID = Convert.ToInt32(dt.Rows[i]["MemTypeID"]);
                    obj.UserTypeName = dt.Rows[i]["MemTypeName"].ToString();
                    list.Add(obj);
                }

                return list;
            }
        }

        public UserType GetUserType(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                UserType obj = new UserType();
                con.Open();
                string query = "select * from MemberType where MemTypeID= " + id + "";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();

                SqlDataAdapter sd = new SqlDataAdapter(cmd);
                sd.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    obj.UserTypeID = Convert.ToInt32((dt.Rows[i][0]).ToString());
                    obj.UserTypeName = dt.Rows[i][1].ToString();
                }
                con.Close();
                return obj;
            }
        }

        public void UpdateUsertype(UserType obj)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "UPDATE MemberType SET MemTypeName = @MemTypeName WHERE MemTypeID = @MemTypeID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MemTypeName", obj.UserTypeName);
                cmd.Parameters.AddWithValue("@MemTypeID", obj.UserTypeID);
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

        public void DeleteUserType(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "DELETE FROM MemberType WHERE MemTypeID=@MemTypeID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MemTypeID", id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}