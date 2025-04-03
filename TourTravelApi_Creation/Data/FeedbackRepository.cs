using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace TourTravelApi_Creation.Data
{
    public class FeedbackRepository
    {
        private readonly string _connectionString;

        public FeedbackRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<FeedbackModel> SelectAll()
        {
            var feedbacks = new List<FeedbackModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Feedback_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    feedbacks.Add(new FeedbackModel
                    {
                        FeedbackID = Convert.ToInt32(reader["FeedbackID"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        FullName = reader["FullName"].ToString(),

                        PackageID = Convert.ToInt32(reader["PackageID"]),
                        PackageName = reader["PackageName"].ToString(),

                        Rating = Convert.ToInt32(reader["Rating"]),
                        Comments = reader["Comments"].ToString(),
                        FeedbackDate = Convert.ToDateTime(reader["FeedbackDate"])
                    });
                }
            }
            return feedbacks;
        }

        public FeedbackModel SelectByPK(int feedbackID)
        {
            FeedbackModel feedback = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Feedback_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@FeedbackID", feedbackID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    feedback = new FeedbackModel
                    {
                        FeedbackID = Convert.ToInt32(reader["FeedbackID"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        FullName = reader["FullName"].ToString(),

                        PackageID = Convert.ToInt32(reader["PackageID"]),
                        PackageName = reader["PackageName"].ToString(),

                        Rating = Convert.ToInt32(reader["Rating"]),
                        Comments = reader["Comments"].ToString(),
                        FeedbackDate = Convert.ToDateTime(reader["FeedbackDate"])

                    };
                }
            }
            return feedback;
        }

        public bool Delete(int feedbackID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Feedback_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@FeedbackID", feedbackID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(FeedbackModel feedback)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Feedback_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", feedback.CustomerID);
                cmd.Parameters.AddWithValue("@PackageID", feedback.PackageID);
                cmd.Parameters.AddWithValue("@Rating", feedback.Rating);
                cmd.Parameters.AddWithValue("@Comments", (object)feedback.Comments ?? DBNull.Value);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
        }

        public bool Update(FeedbackModel feedback)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Feedback_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@FeedbackID", feedback.FeedbackID);
                cmd.Parameters.AddWithValue("@CustomerID", feedback.CustomerID);
                cmd.Parameters.AddWithValue("@PackageID", feedback.PackageID);
                cmd.Parameters.AddWithValue("@Rating", feedback.Rating);
                cmd.Parameters.AddWithValue("@Comments", (object)feedback.Comments ?? DBNull.Value);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public IEnumerable<CustomerDropDownModel> Getcustomers()
        {
            var customers = new List<CustomerDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_DROPDOWN", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new CustomerDropDownModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        FullName = reader["FullName"].ToString()
                    });
                }
            }

            return customers;
        }
        public IEnumerable<PackageDropDownModel> Getpackages()
        {
            var packages = new List<PackageDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Package_DROPDOWN", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    packages.Add(new PackageDropDownModel
                    {
                        PackageID = Convert.ToInt32(reader["PackageID"]),
                        PackageName = reader["PackageName"].ToString()
                    });
                }
            }

            return packages;
        }

    }
}
