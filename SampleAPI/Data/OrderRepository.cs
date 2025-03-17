using SampleAPI.Model;
using System.Data;
using System.Data.SqlClient;

namespace SampleAPI.Data
{
    public class OrderRepository
    {
        private readonly IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GetAllOrders
        public List<OrderModel> GetAllOrders()
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");
            var orders = new List<OrderModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstr))
                {
                    SqlCommand cmd = new SqlCommand("PR_Order_SelectAll", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        orders.Add(new OrderModel
                        {
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            OrderCode = reader["OrderCode"].ToString(),
                            OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                            CustomerID = Convert.ToInt32(reader["CustomerID"]),
                            PaymentMode = reader["PaymentMode"].ToString(),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            ShippingAddress = reader["ShippingAddress"].ToString(),
                            UserID = Convert.ToInt32(reader["UserID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching orders: " + ex.Message);
            }

            return orders;
        }
        #endregion

        #region SelectByPK
        public OrderModel SelectByPk(int orderID)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");
            OrderModel order = null;

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    order = new OrderModel
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderCode = reader["OrderCode"].ToString(),
                        OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                        CustomerID = Convert.ToInt32(reader["CustomerID"]),
                        PaymentMode = reader["PaymentMode"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        ShippingAddress = reader["ShippingAddress"].ToString(),
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
                }
            }
            return order;
        }
        #endregion

        #region Insert
        public bool Insert(OrderModel order)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderCode", order.OrderCode);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                cmd.Parameters.AddWithValue("@PaymentMode", order.PaymentMode);
                cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                cmd.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);
                cmd.Parameters.AddWithValue("@UserID", order.UserID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(OrderModel order)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID", order.OrderID);
                cmd.Parameters.AddWithValue("@OrderCode", order.OrderCode);
                cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                cmd.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                cmd.Parameters.AddWithValue("@PaymentMode", order.PaymentMode);
                cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                cmd.Parameters.AddWithValue("@ShippingAddress", order.ShippingAddress);
                cmd.Parameters.AddWithValue("@UserID", order.UserID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int orderID)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Order_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region DropDown For Customer
        public IEnumerable<CustomerDropDownModel> GetCustomer()
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            var customers = new List<CustomerDropDownModel>();

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Customer_DropDown", conn)
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
                        CustomerName = reader["CustomerName"].ToString()
                    });
                }
            }
            return customers;
        }
        #endregion

        #region DropDown For User
        public IEnumerable<UserDropDownModel> GetUser()
        {
            string connectionStr = _configuration.GetConnectionString("ConnectionString");
            var users = new List<UserDropDownModel>();

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
                    users.Add(new UserDropDownModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString()
                    });
                }
            }
            return users;
        }
        #endregion
    }
}
