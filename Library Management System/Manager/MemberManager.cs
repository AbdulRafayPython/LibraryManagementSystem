using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Library_Management_System.Manager
{
    public class MemberManager
    {
        public void AddMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "INSERT INTO Member (FName, LName, City, MobileNo, EmailID, DOB, MemTypeID) VALUES (@FName, @LName, @City, @MobileNo, @EmailID, @DOB, @MemTypeID)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FName", member.FName);
                cmd.Parameters.AddWithValue("@LName", member.LName);
                cmd.Parameters.AddWithValue("@City", member.City);
                cmd.Parameters.AddWithValue("@MobileNo", member.MobileNo);
                cmd.Parameters.AddWithValue("@EmailID", member.EmailID);
                cmd.Parameters.AddWithValue("@DOB", member.DOB);
                cmd.Parameters.AddWithValue("@MemTypeID", member.MemTypeID);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Member> GetMembers()
        {
            List<Member> list = new List<Member>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
            SELECT m.MemID, m.FName, m.LName, m.City, m.MobileNo, m.EmailID, m.DOB, m.MemTypeID, mt.MemTypeName 
            FROM Member m
            JOIN MemberType mt ON m.MemTypeID = mt.MemTypeID";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Member member = new Member
                    {
                        MemID = Convert.ToInt32(reader["MemID"]),
                        FName = reader["FName"].ToString(),
                        LName = reader["LName"].ToString(),
                        City = reader["City"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        EmailID = reader["EmailID"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        MemTypeID = Convert.ToInt32(reader["MemTypeID"]),
                        MemberType = new UserType
                        {
                            UserTypeID = Convert.ToInt32(reader["MemTypeID"]),
                            UserTypeName = reader["MemTypeName"].ToString() 
                        }
                    };
                    list.Add(member);
                }
            }
            return list;
        }


        public Member GetMember(int id)
        {
            Member member = null;
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
            SELECT m.MemID, m.FName, m.LName, m.City, m.MobileNo, m.EmailID, m.DOB, m.MemTypeID, mt.MemTypeName 
            FROM Member m
            JOIN MemberType mt ON m.MemTypeID = mt.MemTypeID
            WHERE m.MemID = @MemID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MemID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    member = new Member
                    {
                        MemID = Convert.ToInt32(reader["MemID"]),
                        FName = reader["FName"].ToString(),
                        LName = reader["LName"].ToString(),
                        City = reader["City"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        EmailID = reader["EmailID"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        MemTypeID = Convert.ToInt32(reader["MemTypeID"]),
                        MemberType = new UserType
                        {
                            UserTypeID = Convert.ToInt32(reader["MemTypeID"]),
                            UserTypeName = reader["MemTypeName"].ToString()
                        }
                    };
                }
            }
            return member;
        }


        public void UpdateMember(Member member)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "UPDATE Member SET FName = @FName, LName = @LName, City = @City, MobileNo = @MobileNo, EmailID = @EmailID, DOB = @DOB, MemTypeID = @MemTypeID WHERE MemID = @MemID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FName", member.FName);
                cmd.Parameters.AddWithValue("@LName", member.LName);
                cmd.Parameters.AddWithValue("@City", member.City);
                cmd.Parameters.AddWithValue("@MobileNo", member.MobileNo);
                cmd.Parameters.AddWithValue("@EmailID", member.EmailID);
                cmd.Parameters.AddWithValue("@DOB", member.DOB);
                cmd.Parameters.AddWithValue("@MemTypeID", member.MemTypeID);
                cmd.Parameters.AddWithValue("@MemID", member.MemID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMember(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "DELETE FROM Member WHERE MemID = @MemID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MemID", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<UserType> GetMemberTypes()
        {
            List<UserType> memberTypes = new List<UserType>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "SELECT * FROM MemberType";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    UserType userType = new UserType
                    {
                        UserTypeID = Convert.ToInt32(reader["MemTypeID"]),
                        UserTypeName = reader["MemTypeName"].ToString()
                    };
                    memberTypes.Add(userType);
                }
            }
            return memberTypes;
        }




    }
}
