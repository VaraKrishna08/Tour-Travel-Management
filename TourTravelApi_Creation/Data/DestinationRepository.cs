using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;
namespace TourTravelApi_Creation.Data
{
    public class DestinationRepository
    {
  
        private readonly string _connectionString;

        public DestinationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<DestinationModel> SelectAll()
        {
            var destinations = new List<DestinationModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Destination_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    destinations.Add(new DestinationModel
                    {
                        DestinationID = Convert.ToInt32(reader["DestinationID"]),
                        DestinationName = reader["DestinationName"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        Country = reader["Country"].ToString(),
                        State = reader["State"].ToString(),
                        Description = reader["Description"].ToString(),
                        BestTimeToVisit = reader["BestTimeToVisit"].ToString()
                    });
                }
            }
            return destinations;
        }

        public DestinationModel SelectByPK(int DestinationID)
        {
            DestinationModel destination = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Destination_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DestinationID", DestinationID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    destination = new DestinationModel
                    {
                        DestinationID = Convert.ToInt32(reader["DestinationID"]),
                        DestinationName = reader["DestinationName"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        Country = reader["Country"].ToString(),
                        State = reader["State"].ToString(),
                        Description = reader["Description"].ToString(),
                        BestTimeToVisit = reader["BestTimeToVisit"].ToString()
                    };
                }
            }
            return destination;
        }

        public bool Delete(int DestinationID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Destination_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DestinationID", DestinationID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(DestinationModel destination)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Destination_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@DestinationName", destination.DestinationName);
                cmd.Parameters.AddWithValue("@ImageUrl", destination.ImageUrl);
                cmd.Parameters.AddWithValue("@Country", destination.Country);
                cmd.Parameters.AddWithValue("@State", destination.State);
                cmd.Parameters.AddWithValue("@Description", destination.Description);
                cmd.Parameters.AddWithValue("@BestTimeToVisit", destination.BestTimeToVisit); 

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Return true if the insertion was successful
                return rowsAffected > 0;
            }
        }

        public bool Update(DestinationModel destination)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Destination_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DestinationID", destination.DestinationID);
                cmd.Parameters.AddWithValue("@DestinationName", destination.DestinationName);
                cmd.Parameters.AddWithValue("@ImageUrl", destination.ImageUrl);
                cmd.Parameters.AddWithValue("@Country", destination.Country);
                cmd.Parameters.AddWithValue("@State", destination.State);
                cmd.Parameters.AddWithValue("@Description", destination.Description);
                cmd.Parameters.AddWithValue("@BestTimeToVisit", destination.BestTimeToVisit);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}