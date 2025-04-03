using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;
namespace TourTravelApi_Creation.Data
{
    public class PaymentRepository
    {
        private readonly string _connectionString;

        public PaymentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<PaymentModel> SelectAll()
        {
            var payments = new List<PaymentModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    payments.Add(new PaymentModel
                    {
                        PaymentID = Convert.ToInt32(reader["PaymentID"]),
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                        PaymentStatus = reader["PaymentStatus"].ToString(),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"])

                    });
                }
            }
            return payments;
        }

        public PaymentModel SelectByPK(int paymentID)
        {
            PaymentModel payment = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PaymentID", paymentID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    payment = new PaymentModel
                    {
                        PaymentID = Convert.ToInt32(reader["PaymentID"]),
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),

                        PaymentMode = reader["PaymentMode"].ToString(),
                        AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                        PaymentStatus = reader["PaymentStatus"].ToString(),
                        //RegistrationDate = reader["RegistrationDate"]
                    };
                }
            }
            return payment;
        }

        public bool Delete(int paymentID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PaymentID", paymentID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(PaymentModel Payment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookingID", Payment.BookingID);
                cmd.Parameters.AddWithValue("@PaymentMode", Payment.PaymentMode);
                cmd.Parameters.AddWithValue("@AmountPaid", Payment.AmountPaid);
                cmd.Parameters.AddWithValue("@PaymentStatus", Payment.PaymentStatus);
                //cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now); 

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Return true if the insertion was successful
                return rowsAffected > 0;
            }
        }

        public bool Update(PaymentModel Payment)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Payment_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PaymentID", Payment.PaymentID);
                cmd.Parameters.AddWithValue("@BookingID", Payment.BookingID);
                cmd.Parameters.AddWithValue("@PaymentMode", Payment.PaymentMode);
                cmd.Parameters.AddWithValue("@AmountPaid", Payment.AmountPaid);
                cmd.Parameters.AddWithValue("@PaymentStatus", Payment.PaymentStatus);
                //cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now); 

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