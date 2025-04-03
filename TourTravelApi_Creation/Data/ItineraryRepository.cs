using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;
namespace TourTravelApi_Creation.Data
{
    public class ItineraryRepository

    {
        private readonly string _connectionString;

        public ItineraryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ItineraryModel> SelectAll()
        {
            var itinerary = new List<ItineraryModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Itinerary_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    itinerary.Add(new ItineraryModel
                    {
                        ItineraryID = Convert.ToInt32(reader["ItineraryID"]),
                        PackageID = Convert.ToInt32(reader["PackageID"]),
                        PackageName = reader["PackageName"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        DayNumber = Convert.ToInt32(reader["DayNumber"]),
                        Activity = reader["Activity"].ToString(),
                        Location = reader["Location"].ToString(),
                        Time = Convert.ToDateTime(reader["Time"]) // Assuming "Time" is of DateTime type in DB
                    });
                }
            }
            return itinerary;
        }

        public ItineraryModel SelectByPK(int ItineraryID)
        {
            ItineraryModel itinerary = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Itinerary_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ItineraryID", ItineraryID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    itinerary = new ItineraryModel
                    {
                        ItineraryID = Convert.ToInt32(reader["ItineraryID"]),
                        PackageID = Convert.ToInt32(reader["PackageID"]),
                        PackageName = reader["PackageName"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        DayNumber = Convert.ToInt32(reader["DayNumber"]),
                        Activity = reader["Activity"].ToString(),
                        Location = reader["Location"].ToString(),
                        Time = Convert.ToDateTime(reader["Time"]) // Assuming "Time" is of DateTime type in DB
                    };
                }
            }
            return itinerary;
        }

        public bool Delete(int ItineraryID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Itinerary_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ItineraryID", ItineraryID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(ItineraryModel itinerary)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Itinerary_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PackageID", itinerary.PackageID);
                cmd.Parameters.AddWithValue("@ImageUrl", itinerary.ImageUrl);

                cmd.Parameters.AddWithValue("@DayNumber", itinerary.DayNumber);
                cmd.Parameters.AddWithValue("@Activity", itinerary.Activity);
                cmd.Parameters.AddWithValue("@Location", itinerary.Location);
                //cmd.Parameters.AddWithValue("@Time", DateTime.Now); 

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Return true if the insertion was successful
                return rowsAffected > 0;
            }
        }

        public bool Update(ItineraryModel itinerary)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Itinerary_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ItineraryID", itinerary.ItineraryID);
                cmd.Parameters.AddWithValue("@PackageID", itinerary.PackageID);
                cmd.Parameters.AddWithValue("@ImageUrl", itinerary.ImageUrl);

                cmd.Parameters.AddWithValue("@DayNumber", itinerary.DayNumber);
                cmd.Parameters.AddWithValue("@Activity", itinerary.Activity);
                cmd.Parameters.AddWithValue("@Location", itinerary.Location);
                //cmd.Parameters.AddWithValue("@Time", DateTime.Now); 

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
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
