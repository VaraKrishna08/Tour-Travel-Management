using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;

namespace TourTravelApi_Creation.Data
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<EmployeeModel> SelectAll()
        {
            var employees = new List<EmployeeModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Employee_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(new EmployeeModel
                    {
                        EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                        FullName = reader["FullName"].ToString(),
                        Position = reader["Position"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    });
                }
            }
            return employees;
        }

        public EmployeeModel SelectByPK(int EmployeeID)
        {
            EmployeeModel employees = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Employee_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    employees = new EmployeeModel
                    {
                        EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                        FullName = reader["FullName"].ToString(),
                        Position = reader["Position"].ToString(),
                        ContactNumber = reader["ContactNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        Salary = Convert.ToDecimal(reader["Salary"])
                    };
                }
            }
            return employees;
        }

        public bool Delete(int EmployeeID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Employee_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(EmployeeModel employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Employee_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@ContactNumber", employee.ContactNumber);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary); 

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Execute the stored procedure

                // Return true if the insertion was successful
                return rowsAffected > 0;
            }
        }

        public bool Update(EmployeeModel employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Employee_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@ContactNumber", employee.ContactNumber);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}
