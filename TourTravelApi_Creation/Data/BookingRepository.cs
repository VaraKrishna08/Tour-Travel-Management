using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;

    namespace TourTravelApi_Creation.Data
{
    public class BookingRepository
    {
        private readonly string _connectionString;

        public BookingRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<BookingModel> SelectAll()
        {
            var bookings = new List<BookingModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Booking_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bookings.Add(new BookingModel
                    {
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        FullName = reader["FullName"].ToString(),

                        PackageID = Convert.ToInt32(reader["PackageID"]),
                        PackageName = reader["PackageName"].ToString(),

                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                        TravelDate = Convert.ToDateTime(reader["TravelDate"]),
                        NumberOfPeople = Convert.ToInt32(reader["NumberOfPeople"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Status = reader["Status"].ToString()

                    });
                }
            }
            return bookings;
        }

        public BookingModel SelectByPK(int BookingID)
        {
            BookingModel booking = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Booking_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookingID", BookingID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    booking = new BookingModel
                    {
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        FullName = reader["FullName"].ToString(),

                        PackageID = Convert.ToInt32(reader["PackageID"]),
                        PackageName = reader["PackageName"].ToString(),

                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                        TravelDate = Convert.ToDateTime(reader["TravelDate"]),
                        NumberOfPeople = Convert.ToInt32(reader["NumberOfPeople"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Status = reader["Status"].ToString()
                    };
                }
            }
            return booking;
        }

        public bool Delete(int BookingID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Booking_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookingID", BookingID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(BookingModel booking)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Booking_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", booking.CustomerID);
                cmd.Parameters.AddWithValue("@PackageID", booking.PackageID);
                //cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@TravelDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@NumberOfPeople",booking.NumberOfPeople);
                cmd.Parameters.AddWithValue("@TotalAmount", booking.TotalAmount);
                cmd.Parameters.AddWithValue("@Status", booking.Status);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Return true if the insertion was successful
                return rowsAffected > 0;
            }
        }

        public bool Update(BookingModel booking)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Booking_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BookingID", booking.BookingID);
                cmd.Parameters.AddWithValue("@CustomerID", booking.CustomerID);
                cmd.Parameters.AddWithValue("@PackageID", booking.PackageID);
                //cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@TravelDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@NumberOfPeople", booking.NumberOfPeople);
                cmd.Parameters.AddWithValue("@TotalAmount", booking.TotalAmount);
                cmd.Parameters.AddWithValue("@Status", booking.Status);
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