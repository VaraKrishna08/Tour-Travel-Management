using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;
namespace TourTravelApi_Creation.Data
{
       public class PackageRepository
         {
        private readonly string _connectionString;

        public PackageRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<PackageModel> SelectAll()
        {
            var packages = new List<PackageModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Package_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    packages.Add(new PackageModel
                    {
                        PackageID = Convert.ToInt32(reader["PackageID"]),
                        PackageName = reader["PackageName"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        Description = reader["Description"].ToString(),
                        DestinationID = Convert.ToInt32(reader["DestinationID"]),
                        DestinationName = reader["DestinationName"].ToString(), // Fetch DestinationName

                        Price = Convert.ToDecimal(reader["Price"]),
                        Duration = Convert.ToInt32(reader["Duration"]),
                        Status = reader["Status"].ToString()
                    });
                }
            }
            return packages;
        }

        public PackageModel SelectByPK(int PackageID)
        {
            PackageModel package = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Package_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PackageID", PackageID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    package = new PackageModel
                    {
                        PackageID = Convert.ToInt32(reader["PackageID"]),
                        PackageName = reader["PackageName"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        Description = reader["Description"].ToString(),
                        DestinationID = Convert.ToInt32(reader["DestinationID"]),
                        DestinationName = reader["DestinationName"].ToString(), // Fetch DestinationName

                        Price = Convert.ToDecimal(reader["Price"]),
                        Duration = Convert.ToInt32(reader["Duration"]),
                        Status = reader["Status"].ToString()
                    };
                }
            }
            return package;
        }

        public bool Delete(int PackageID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Package_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PackageID", PackageID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(PackageModel package)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Package_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@PackageName", package.PackageName);
                cmd.Parameters.AddWithValue("@ImageUrl", package.ImageUrl);
                cmd.Parameters.AddWithValue("@Description", package.Description);
                cmd.Parameters.AddWithValue("@DestinationID", package.DestinationID);
                cmd.Parameters.AddWithValue("@Price", package.Price);
                cmd.Parameters.AddWithValue("@Duration", package.Duration);
                cmd.Parameters.AddWithValue("@Status", package.Status);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Return true if the insertion was successful
                return rowsAffected > 0;
            }
        }

        public bool Update(PackageModel package)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Package_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PackageID", package.PackageID);
                cmd.Parameters.AddWithValue("@PackageName", package.PackageName);
                cmd.Parameters.AddWithValue("@ImageUrl", package.ImageUrl);

                cmd.Parameters.AddWithValue("@Description", package.Description);
                cmd.Parameters.AddWithValue("@DestinationID", package.DestinationID);
                cmd.Parameters.AddWithValue("@Price", package.Price);
                cmd.Parameters.AddWithValue("@Duration", package.Duration);
                cmd.Parameters.AddWithValue("@Status", package.Status);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public IEnumerable<DestinationDropDownModel> Getdestinations()
        {
            var destinations = new List<DestinationDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Destination_DROPDOWN", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    destinations.Add(new DestinationDropDownModel
                    {
                        DestinationID = Convert.ToInt32(reader["DestinationID"]),
                        DestinationName = reader["DestinationName"].ToString()
                    });
                }
            }

            return destinations;
        }

    }
}