using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Library_Management_System.Manager
{
    public class FinePolicyManager
    {
        public void AddFinePolicy(FinePolicy finePolicy)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "INSERT INTO FinePolicy (MaxDays, FineAmount, MemTypeID) VALUES (@MaxDays, @FineAmount, @MemTypeID)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MaxDays", finePolicy.MaxDays);
                cmd.Parameters.AddWithValue("@FineAmount", finePolicy.FineAmount);
                cmd.Parameters.AddWithValue("@MemTypeID", finePolicy.MemTypeID);
                cmd.ExecuteNonQuery();
            }
        }

        public List<FinePolicy> GetFinePolicies()
        {
            List<FinePolicy> list = new List<FinePolicy>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
                SELECT fp.FinePolicyID, fp.MaxDays, fp.FineAmount, fp.MemTypeID, mt.MemTypeName
                FROM FinePolicy fp
                JOIN MemberType mt ON fp.MemTypeID = mt.MemTypeID";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FinePolicy finePolicy = new FinePolicy
                    {
                        FinePolicyID = Convert.ToInt32(reader["FinePolicyID"]),
                        MaxDays = Convert.ToInt32(reader["MaxDays"]),
                        FineAmount = Convert.ToDecimal(reader["FineAmount"]),
                        MemTypeID = Convert.ToInt32(reader["MemTypeID"]),
                        MemberType = new UserType
                        {
                            UserTypeID = Convert.ToInt32(reader["MemTypeID"]),
                            UserTypeName = reader["MemTypeName"].ToString()
                        }
                    };
                    list.Add(finePolicy);
                }
            }
            return list;
        }

        public FinePolicy GetFinePolicy(int id)
        {
            FinePolicy finePolicy = null;
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
                SELECT fp.FinePolicyID, fp.MaxDays, fp.FineAmount, fp.MemTypeID, mt.MemTypeName
                FROM FinePolicy fp
                JOIN MemberType mt ON fp.MemTypeID = mt.MemTypeID
                WHERE fp.FinePolicyID = @FinePolicyID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FinePolicyID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    finePolicy = new FinePolicy
                    {
                        FinePolicyID = Convert.ToInt32(reader["FinePolicyID"]),
                        MaxDays = Convert.ToInt32(reader["MaxDays"]),
                        FineAmount = Convert.ToDecimal(reader["FineAmount"]),
                        MemTypeID = Convert.ToInt32(reader["MemTypeID"]),
                        MemberType = new UserType
                        {
                            UserTypeID = Convert.ToInt32(reader["MemTypeID"]),
                            UserTypeName = reader["MemTypeName"].ToString()
                        }
                    };
                }
            }
            return finePolicy;
        }

        public void UpdateFinePolicy(FinePolicy finePolicy)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "UPDATE FinePolicy SET MaxDays = @MaxDays, FineAmount = @FineAmount, MemTypeID = @MemTypeID WHERE FinePolicyID = @FinePolicyID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FinePolicyID", finePolicy.FinePolicyID);
                cmd.Parameters.AddWithValue("@MaxDays", finePolicy.MaxDays);
                cmd.Parameters.AddWithValue("@FineAmount", finePolicy.FineAmount);
                cmd.Parameters.AddWithValue("@MemTypeID", finePolicy.MemTypeID);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteFinePolicy(int id)
        {
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = "DELETE FROM FinePolicy WHERE FinePolicyID = @FinePolicyID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FinePolicyID", id);
                cmd.ExecuteNonQuery();
            }
        }

        public List<FinePolicy> GetFinePolicyByMemTypeID(int memTypeID)
        {
            List<FinePolicy> list = new List<FinePolicy>();
            using (SqlConnection con = new SqlConnection(DatabaseManager.conpath))
            {
                con.Open();
                string query = @"
            SELECT fp.FinePolicyID, fp.MaxDays, fp.FineAmount, fp.MemTypeID, mt.MemTypeName
            FROM FinePolicy fp
            JOIN MemberType mt ON fp.MemTypeID = mt.MemTypeID
            WHERE fp.MemTypeID = @MemTypeID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@MemTypeID", memTypeID);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FinePolicy finePolicy = new FinePolicy
                    {
                        FinePolicyID = Convert.ToInt32(reader["FinePolicyID"]),
                        MaxDays = Convert.ToInt32(reader["MaxDays"]),
                        FineAmount = Convert.ToDecimal(reader["FineAmount"]),
                        MemTypeID = Convert.ToInt32(reader["MemTypeID"]),
                        MemberType = new UserType
                        {
                            UserTypeID = Convert.ToInt32(reader["MemTypeID"]),
                            UserTypeName = reader["MemTypeName"].ToString()
                        }
                    };
                    list.Add(finePolicy);
                }
            }
            return list;
        }

        public decimal CalculateFineAmount(int memTypeID, int overdueDays)
        {
            decimal fineAmount = 0;
            List<FinePolicy> policies = GetFinePolicyByMemTypeID(memTypeID);

            foreach (FinePolicy policy in policies)
            {
                if (overdueDays <= policy.MaxDays)
                {
                    fineAmount = policy.FineAmount;
                    break;
                }
            }

            return fineAmount;
        }


    }
}