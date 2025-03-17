using SampleAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace SampleAPI.Data
{
    public class CustomerRepository
    {
        private readonly IConfiguration _configuration;

        public CustomerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GetAll
        public List<CustomerModel> GetAllCustomers()
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");
            var customers = new List<CustomerModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstr))
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
                            CustomerName = reader["CustomerName"].ToString(),
                            HomeAddress = reader["HomeAddress"].ToString(),
                            Email = reader["Email"].ToString(),
                            MobileNo = reader["MobileNo"].ToString(),
                            GST_NO = reader["GST_NO"].ToString(),
                            CityName = reader["CityName"].ToString(),
                            PinCode = reader["PinCode"].ToString(),
                            NetAmount = reader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(reader["NetAmount"]) : (decimal?)null,
                            UserID = Convert.ToInt32(reader["UserID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching customers: " + ex.Message);
            }

            return customers;
        }
        #endregion

        #region SelectByPk
        public CustomerModel SelectByPk(int customerID)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");
            CustomerModel customer = null;

            using (SqlConnection conn = new SqlConnection(connectionstr))
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
                        CustomerName = reader["CustomerName"].ToString(),
                        HomeAddress = reader["HomeAddress"].ToString(),
                        Email = reader["Email"].ToString(),
                        MobileNo = reader["MobileNo"].ToString(),
                        GST_NO = reader["GST_NO"].ToString(),
                        CityName = reader["CityName"].ToString(),
                        PinCode = reader["PinCode"].ToString(),
                        NetAmount = reader["NetAmount"] != DBNull.Value ? Convert.ToDecimal(reader["NetAmount"]) : (decimal?)null,
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
                }
            }
            return customer;
        }
        #endregion

        #region Insert
        public bool Insert(CustomerModel customer)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@HomeAddress", customer.HomeAddress);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                cmd.Parameters.AddWithValue("@GST_NO", customer.GST_NO);
                cmd.Parameters.AddWithValue("@CityName", customer.CityName);
                cmd.Parameters.AddWithValue("@PinCode", customer.PinCode);
                cmd.Parameters.AddWithValue("@NetAmount", customer.NetAmount.HasValue ? customer.NetAmount.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UserID", customer.UserID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(CustomerModel customer)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@HomeAddress", customer.HomeAddress);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
                cmd.Parameters.AddWithValue("@GST_NO", customer.GST_NO);
                cmd.Parameters.AddWithValue("@CityName", customer.CityName);
                cmd.Parameters.AddWithValue("@PinCode", customer.PinCode);
                cmd.Parameters.AddWithValue("@NetAmount", customer.NetAmount.HasValue ? customer.NetAmount.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UserID", customer.UserID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int customerID)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
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
        #endregion

        #region DropDown For User
        public IEnumerable<UserDropDownModel> GetCustomer()
        {
            string connectionStr = _configuration.GetConnectionString("ConnectionString");
            var customers = new List<UserDropDownModel>();

            using (SqlConnection conn = new SqlConnection(connectionStr))
            {
                SqlCommand cmd = new SqlCommand("PR_User_DropDown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new UserDropDownModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString()
                    });
                }
            }
            return customers;
        }
        #endregion
    }
}