using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
namespace TourTravelApi_Creation.Data
{
    public class OfferRepository
    {
        private readonly string _connectionString;

        public OfferRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<OfferModel> SelectAll()
        {
            var offers = new List<OfferModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Offers_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    offers.Add(new OfferModel
                    {
                        OfferID = Convert.ToInt32(reader["OfferID"]),
                        OfferName = reader["OfferName"].ToString(),
                        Description = reader["Description"].ToString(),
                        DiscountPercentage = Convert.ToDecimal(reader["DiscountPercentage"]),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                        ApplicablePackages = reader["ApplicablePackages"].ToString()

                    });
                }
            }
            return offers;
        }

        public OfferModel SelectByPK(int OfferID)
        {
            OfferModel offer = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Offers_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OfferID", OfferID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    offer = new OfferModel
                    {
                        OfferID = Convert.ToInt32(reader["OfferID"]),
                        OfferName = reader["OfferName"].ToString(),
                        Description = reader["Description"].ToString(),
                        DiscountPercentage = Convert.ToDecimal(reader["DiscountPercentage"]),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                        ApplicablePackages = reader["ApplicablePackages"].ToString()

                    };
                }
            }
            return offer;
        }

        public bool Delete(int OfferID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Offers_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OfferID", OfferID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(OfferModel offer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Offers_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OfferName", offer.OfferName);
                cmd.Parameters.AddWithValue("@Description", offer.Description);
                cmd.Parameters.AddWithValue("@DiscountPercentage", offer.DiscountPercentage);
                cmd.Parameters.AddWithValue("@EndDate",offer.EndDate);
                cmd.Parameters.AddWithValue("@ApplicablePackages", offer.ApplicablePackages);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Return true if the insertion was successful
                return rowsAffected > 0;
            }
        }

        public bool Update(OfferModel offer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Offers_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OfferID", offer.OfferID);
                cmd.Parameters.AddWithValue("@OfferName", offer.OfferName);
                cmd.Parameters.AddWithValue("@Description", offer.Description);
                cmd.Parameters.AddWithValue("@DiscountPercentage", offer.DiscountPercentage);
                cmd.Parameters.AddWithValue("@EndDate", offer.EndDate);
                cmd.Parameters.AddWithValue("@ApplicablePackages", offer.ApplicablePackages);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    } }