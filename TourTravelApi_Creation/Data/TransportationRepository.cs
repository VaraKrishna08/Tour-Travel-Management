using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System;
namespace TourTravelApi_Creation.Data
{
    public class TransportationRepository
    {
        private readonly string _connectionString;

        public TransportationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<TransportationModel> SelectAll()
        {
            var transportations = new List<TransportationModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Transportation_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    transportations.Add(new TransportationModel
                    {
                        TransportID = Convert.ToInt32(reader["TransportID"]),
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),

                        TransportMode = reader["TransportMode"].ToString(),
                        TransportDetails = reader["TransportDetails"].ToString(),
                        DepartureTime = Convert.ToDateTime(reader["DepartureTime"]),
                        ArrivalTime = Convert.ToDateTime(reader["ArrivalTime"]),
                        Cost = Convert.ToDecimal(reader["Cost"])



                    });
                }
            }
            return transportations;
        }

        public TransportationModel SelectByPK(int transportID)
        {
            TransportationModel Transportation = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Transportation_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@TransportID", transportID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Transportation = new TransportationModel
                    {
                        TransportID = Convert.ToInt32(reader["TransportID"]),
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),

                        TransportMode = reader["TransportMode"].ToString(),
                        TransportDetails = reader["TransportDetails"].ToString(),
                        DepartureTime = Convert.ToDateTime(reader["DepartureTime"]),
                        ArrivalTime = Convert.ToDateTime(reader["ArrivalTime"]),
                        Cost = Convert.ToDecimal(reader["Cost"])

                    };
                }
            }
            return Transportation;
        }

        public bool Delete(int transportID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Transportation_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@TransportID", transportID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(TransportationModel transportation)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Transportation_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookingID", transportation.BookingID);
                cmd.Parameters.AddWithValue("@TransportMode", transportation.TransportMode);
                cmd.Parameters.AddWithValue("@TransportDetails", transportation.TransportDetails);
                cmd.Parameters.AddWithValue("@DepartureTime", transportation.DepartureTime);
                cmd.Parameters.AddWithValue("@ArrivalTime", transportation.ArrivalTime);
                cmd.Parameters.AddWithValue("@Cost", transportation.Cost);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Return true if the insertion was successful
                return rowsAffected > 0;
            }
        }

        public bool Update(TransportationModel transportation)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Transportation_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@TransportID", transportation.TransportID);
                cmd.Parameters.AddWithValue("@BookingID", transportation.BookingID);
                cmd.Parameters.AddWithValue("@TransportMode", transportation.TransportMode);
                cmd.Parameters.AddWithValue("@TransportDetails", transportation.TransportDetails);
                cmd.Parameters.AddWithValue("@DepartureTime", transportation.DepartureTime);
                cmd.Parameters.AddWithValue("@ArrivalTime", transportation.ArrivalTime);
                cmd.Parameters.AddWithValue("@Cost", transportation.Cost);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public IEnumerable<BookingDropDownModel> Getbookings()
        {
            var bookings = new List<BookingDropDownModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Booking_DROPDOWN", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    bookings.Add(new BookingDropDownModel
                    {
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"])
                    });
                }
            }

            return bookings;
        }
    }
}