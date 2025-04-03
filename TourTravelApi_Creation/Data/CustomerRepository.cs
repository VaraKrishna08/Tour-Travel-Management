using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TourTravelApi_Creation.Models;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace TourTravelApi_Creation.Data
{
    public class CustomerRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<CustomerModel> SelectAll()
        {
            var customers = new List<CustomerModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_SelectAll", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    customers.Add(new CustomerModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        FullName = reader["FullName"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
                        Password = reader["Password"].ToString()
                    });
                }
            }
            return customers;
        }

        public CustomerModel SelectByPK(int customerID)
        {
            CustomerModel customer = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new CustomerModel
                    {
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        FullName = reader["FullName"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
                        Password = reader["Password"].ToString()
                    };
                }
            }
            return customer;
        }

        public bool Delete(int customerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CustomerID", customerID);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Insert(CustomerModel customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@FullName", customer.FullName);
                cmd.Parameters.AddWithValue("@ImageUrl", customer.ImageUrl);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                //cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now); 
                cmd.Parameters.AddWithValue("@Password", customer.Password);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        public bool Update(CustomerModel customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@FullName", customer.FullName);
                cmd.Parameters.AddWithValue("@ImageUrl", customer.ImageUrl);

                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                cmd.Parameters.AddWithValue("@Password", customer.Password);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        //public CustomerModel GetCustomerByEmail(string email)
        //{
        //    CustomerModel customer = null;
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("SELECT CustomerID, FullName, Email, Password FROM Customers WHERE Email = @Email", conn);
        //        cmd.Parameters.AddWithValue("@Email", email);

        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            customer = new CustomerModel
        //            {
        //                CustomerID = Convert.ToInt32(reader["CustomerID"]),
        //                FullName = reader["FullName"].ToString(),
        //                Email = reader["Email"].ToString(),
        //                Password = reader["Password"].ToString()
        //            };
        //        }
        //    }
        //    return customer;
        //}



        #region Signup
        public bool Signup(CustomerModel customerModel, out string errorMessage)
        {
            errorMessage = string.Empty;

            //// Check if the email is already registered
            //if (GetCustomerByEmail(customerModel.Email) != null)
            //{
            //    errorMessage = "Email already exists.";
            //    return false;
            //}

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Register", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@FullName", customerModel.FullName);
                cmd.Parameters.AddWithValue("@Email", customerModel.Email);
                cmd.Parameters.AddWithValue("@Phone", customerModel.Phone);
                cmd.Parameters.AddWithValue("@Address", customerModel.Address);
                cmd.Parameters.AddWithValue("@Password", customerModel.Password);
                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Rows Affected: {rowsAffected}");

                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL Error: {ex.Message}");
                    errorMessage = $"SQL Error: {ex.Message}";
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected Error: {ex.Message}");
                    errorMessage = $"An unexpected error occurred: {ex.Message}";
                    return false;
                }
            }
        }
        #endregion

        #region Login
        public string Login(LoginModel loginModel, out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand("PR_Customer_Login", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", loginModel.Email);
                    cmd.Parameters.AddWithValue("@Password", loginModel.Password);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // If login is successful
                        {
                            var customer = new CustomerModel
                            {
                                CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString()
                            };

                            return GenerateJwtToken(customer); // Fixed variable name to 'user'
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                errorMessage = ex.Message; // Capture error message from stored procedure
                return null;
            }
            catch (Exception ex)
            {
                errorMessage = "An unexpected error occurred. Please try again.";
                return null;
            }

            errorMessage = "Invalid username or password.";
            return null;
        }

        private string GenerateJwtToken(CustomerModel customer)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, customer.Email),
                //new Claim(ClaimTypes.Role, user.Role),
                new Claim("CustomerID", customer.CustomerID.ToString())
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
//    #region
//    public string Login(LoginModel loginModel, out string errorMessage)
//    {
//        errorMessage = string.Empty;
//        try
//        {
//            using (SqlConnection conn = new SqlConnection(_connectionString))
//            using (SqlCommand cmd = new SqlCommand("PR_Customer_Login", conn))
//            {
//                cmd.CommandType = CommandType.StoredProcedure;
//                cmd.Parameters.AddWithValue("@Email", loginModel.Email);
//                cmd.Parameters.AddWithValue("@Password", loginModel.Password);

//                conn.Open();

//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    if (reader.Read())  // If a record is found
//                    {
//                        // Debug log
//                        Console.WriteLine("Customer found!");

//                        var customer = new CustomerModel
//                        {
//                            CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
//                            FullName = reader["FullName"]?.ToString(),
//                            Email = reader["Email"]?.ToString()
//                        };

//                        return GenerateJwtToken(customer);
//                    }
//                    else
//                    {
//                        Console.WriteLine("No customer found for the given credentials.");
//                    }
//                }
//            }
//        }
//        catch (SqlException ex)
//        {
//            errorMessage = "Database error: " + ex.Message;
//            return null;
//        }
//        catch (Exception ex)
//        {
//            errorMessage = "An unexpected error occurred: " + ex.Message;
//            return null;
//        }

//        errorMessage = "Invalid email or password.";
//        return null;
//    }
//    #endregion

//    private string GenerateJwtToken(CustomerModel customer)
//    {
//        if (customer == null)
//        {
//            Console.WriteLine("Error generating JWT: Customer object is null.");
//            throw new Exception("Error generating JWT: Customer object is null.");
//        }

//        var jwtKey = _configuration["Jwt:Key"];
//        var jwtIssuer = _configuration["Jwt:Issuer"];
//        var jwtAudience = _configuration["Jwt:Audience"];

//        if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
//        {
//            Console.WriteLine("Error generating JWT: JWT configuration is missing.");
//            throw new Exception("JWT configuration is missing.");
//        }

//        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
//        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//        var claims = new[]
//        {
//    new Claim(ClaimTypes.NameIdentifier, customer.CustomerID.ToString()),
//    new Claim(ClaimTypes.Email, customer.Email),
//    new Claim(ClaimTypes.Name, customer.FullName)
//};

//        var token = new JwtSecurityToken(
//            issuer: jwtIssuer,
//            audience: jwtAudience,
//            claims: claims,
//            expires: DateTime.UtcNow.AddHours(1),
//            signingCredentials: credentials
//        );

//        return new JwtSecurityTokenHandler().WriteToken(token);
//    }


//public bool Signup(CustomerModel customerModel, out string errorMessage)
//{
//    errorMessage = string.Empty;

//    //// Check if the email is already registered
//    //if (GetCustomerByEmail(customerModel.Email) != null)
//    //{
//    //    errorMessage = "Email already exists.";
//    //    return false;
//    //}

//    using (SqlConnection conn = new SqlConnection(_connectionString))
//    {
//        SqlCommand cmd = new SqlCommand("PR_Customer_Register", conn)
//        {
//            CommandType = CommandType.StoredProcedure
//        };

//        cmd.Parameters.AddWithValue("@FullName", customerModel.FullName);
//        cmd.Parameters.AddWithValue("@Email", customerModel.Email);
//        cmd.Parameters.AddWithValue("@Phone", customerModel.Phone);
//        cmd.Parameters.AddWithValue("@Address", customerModel.Address);
//        cmd.Parameters.AddWithValue("@Password", customerModel.Password);
//        try
//        {
//            conn.Open();
//            int rowsAffected = cmd.ExecuteNonQuery();
//            Console.WriteLine($"Rows Affected: {rowsAffected}");

//            return true;
//        }
//        catch (SqlException ex)
//        {
//            Console.WriteLine($"SQL Error: {ex.Message}");
//            errorMessage = $"SQL Error: {ex.Message}";
//            return false;
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Unexpected Error: {ex.Message}");
//            errorMessage = $"An unexpected error occurred: {ex.Message}";
//            return false;
//        }
//    }
//    }

//using System.Data;
//using System.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using TourTravelApi_Creation.Models;
//using Microsoft.Data.SqlClient;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//namespace TourTravelApi_Creation.Data
//{
//    public class CustomerRepository
//    {
//        private readonly string _connectionString;
//        private readonly IConfiguration _configuration;

//        public CustomerRepository(IConfiguration configuration)
//        {
//            _connectionString = _configuration.GetConnectionString("DefaultConnection");

//        }

//        public IEnumerable<CustomerModel> SelectAll()
//        {
//            Console.WriteLine($"Database Connection String: {_connectionString}");

//            var customers = new List<CustomerModel>();
//            using (SqlConnection conn = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("PR_Customer_SelectAll", conn)
//                {
//                    CommandType = CommandType.StoredProcedure
//                };
//                conn.Open();
//                SqlDataReader reader = cmd.ExecuteReader();
//                while (reader.Read())
//                {
//                    customers.Add(new CustomerModel
//                    {
//                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
//                        FullName = reader["FullName"].ToString(),
//                        Email = reader["Email"].ToString(),
//                        Phone = reader["Phone"].ToString(),
//                        Address = reader["Address"].ToString(),
//                        RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
//                        Password = reader["Password"].ToString()
//                    });
//                }
//            }
//            return customers;
//        }

//        public CustomerModel SelectByPK(int customerID)
//        {
//            CustomerModel customer = null;
//            using (SqlConnection conn = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("PR_Customer_SelectByPK", conn)
//                {
//                    CommandType = CommandType.StoredProcedure
//                };
//                cmd.Parameters.AddWithValue("@CustomerID", customerID);
//                conn.Open();
//                SqlDataReader reader = cmd.ExecuteReader();
//                if (reader.Read())
//                {
//                    customer = new CustomerModel
//                    {
//                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
//                        FullName = reader["FullName"].ToString(),
//                        Email = reader["Email"].ToString(),
//                        Phone = reader["Phone"].ToString(),
//                        Address = reader["Address"].ToString(),
//                        RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
//                        Password = reader["Password"].ToString()
//                    };
//                }
//            }
//            return customer;
//        }

//        public bool Delete(int customerID)
//        {
//            using (SqlConnection conn = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("PR_Customer_DeleteByPK", conn)
//                {
//                    CommandType = CommandType.StoredProcedure
//                };
//                cmd.Parameters.AddWithValue("@CustomerID", customerID);
//                conn.Open();
//                int rowsAffected = cmd.ExecuteNonQuery();
//                return rowsAffected > 0;
//            }
//        }

//        public bool Insert(CustomerModel customer)
//        {
//            using (SqlConnection conn = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("PR_Customer_Insert", conn)
//                {
//                    CommandType = CommandType.StoredProcedure
//                };

//                cmd.Parameters.AddWithValue("@FullName", customer.FullName);
//                cmd.Parameters.AddWithValue("@Email", customer.Email);
//                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
//                cmd.Parameters.AddWithValue("@Address", customer.Address);
//                //cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now); 
//                cmd.Parameters.AddWithValue("@Password",customer.Password); 
//                conn.Open();
//                int rowsAffected = cmd.ExecuteNonQuery(); 
//                return rowsAffected > 0;
//            }
//        }

//        public bool Update(CustomerModel customer)
//        {
//            using (SqlConnection conn = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("PR_Customer_UpdateByPK", conn)
//                {
//                    CommandType = CommandType.StoredProcedure
//                };
//                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
//                cmd.Parameters.AddWithValue("@FullName", customer.FullName);
//                cmd.Parameters.AddWithValue("@Email", customer.Email);
//                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
//                cmd.Parameters.AddWithValue("@Address", customer.Address);
//                //cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now); 
//                cmd.Parameters.AddWithValue("@Password", customer.Password);

//                conn.Open();
//                int rowsAffected = cmd.ExecuteNonQuery();
//                return rowsAffected > 0;
//            }
//        }

//        public CustomerModel GetCustomerByEmail(string email)
//        {
//            CustomerModel customer = null;
//            using (SqlConnection conn = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("SELECT CustomerID, FullName, Email, Password FROM Customers WHERE Email = @Email", conn);
//                cmd.Parameters.AddWithValue("@Email", email);

//                conn.Open();
//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    if (reader.HasRows && reader.Read()) // Prevents accessing invalid data
//                    {
//                        customer = new CustomerModel
//                        {
//                            CustomerID = reader["CustomerID"] != DBNull.Value ? Convert.ToInt32(reader["CustomerID"]) : 0,
//                            FullName = reader["FullName"]?.ToString() ?? "",
//                            Email = reader["Email"]?.ToString() ?? "",
//                            Password = reader["Password"]?.ToString() ?? ""
//                        };
//                    }
//                }
//            }
//            return customer;
//        }



//        #region Signup
//        public bool Signup(CustomerModel customerModel, out string errorMessage)
//        {
//            errorMessage = string.Empty;

//            //Console.WriteLine($"Connection String: {_connectionString}");


//            if (GetCustomerByEmail(customerModel.Email) != null)
//            {
//                errorMessage = "Email already exists.";
//                return false;
//            }

//            using (SqlConnection conn = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("PR_Customer_Register", conn)
//                {
//                    CommandType = CommandType.StoredProcedure
//                };

//                cmd.Parameters.AddWithValue("@FullName", customerModel.FullName);
//                cmd.Parameters.AddWithValue("@Email", customerModel.Email);
//                cmd.Parameters.AddWithValue("@Phone", customerModel.Phone);
//                cmd.Parameters.AddWithValue("@Address", customerModel.Address);
//                cmd.Parameters.AddWithValue("@Password", customerModel.Password);

//                try
//                {
//                    conn.Open();
//                    int rowsAffected = cmd.ExecuteNonQuery();
//                    Console.WriteLine($"Rows Affected: {rowsAffected}");

//                    return true;
//                }
//                catch (SqlException ex)
//                {
//                    Console.WriteLine($"SQL Error: {ex.Message}");  
//                    errorMessage = $"SQL Error: {ex.Message}";
//                    return false;
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Unexpected Error: {ex.Message}"); 
//                    errorMessage = $"An unexpected error occurred: {ex.Message}";
//                    return false;
//                }
//            }
//        }
//        #endregion
//           #region Login

//        public string Login(LoginModel loginModel, out string errorMessage)
//        {
//            errorMessage = string.Empty;
//            try
//            {
//                using (SqlConnection conn = new SqlConnection(_connectionString))
//                using (SqlCommand cmd = new SqlCommand("PR_Customer_Login", conn))
//                {
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.Parameters.AddWithValue("@Email", loginModel.Email);
//                    cmd.Parameters.AddWithValue("@Password", loginModel.Password);

//                    conn.Open();

//                    using (SqlDataReader reader = cmd.ExecuteReader())
//                    {
//                        if (reader.HasRows && reader.Read())
//                        {
//                            var customer = new CustomerModel
//                            {
//                                CustomerID = Convert.ToInt32(reader["CustomerID"]),
//                                Email = reader["Email"].ToString(),
//                                Password = reader["Password"].ToString()
//                            };

//                            return GenerateJwtToken(customer);
//                        }
//                    }

//                }
//            }

//            catch (SqlException ex)
//            {
//                errorMessage = ex.Message;
//                Console.WriteLine($"SQL Exception: {errorMessage}"); 
//                return null;
//            }
//            catch (Exception ex)
//            {
//                errorMessage = "An unexpected error occurred. Please try again.";
//                Console.WriteLine($"Exception: {ex.Message}");
//                return null;
//            }

//            errorMessage = "Invalid email or password.";
//            Console.WriteLine(errorMessage); 
//            return null;
//        }

//        private string GenerateJwtToken(CustomerModel customer)
//        {
//            if (string.IsNullOrEmpty(_configuration["Jwt:Key"]) ||
//                string.IsNullOrEmpty(_configuration["Jwt:Issuer"]) ||
//                string.IsNullOrEmpty(_configuration["Jwt:Audience"]))
//            {
//                throw new InvalidOperationException("JWT settings are missing in the configuration.");
//            }

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//        new Claim(ClaimTypes.Email, customer.Email),
//        new Claim("CustomerID", customer.CustomerID.ToString())
//    };

//            var token = new JwtSecurityToken(
//                _configuration["Jwt:Issuer"],
//                _configuration["Jwt:Audience"],
//                claims,
//                expires: DateTime.UtcNow.AddDays(7),
//                signingCredentials: credentials);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//        #endregion

//    }


//}
