using SampleAPI.Model;
using System.Data;
using System.Data.SqlClient;
using SampleAPI.Data;

namespace SampleAPI.Data
{
    public class OrderDetailRepository
    {
        private readonly IConfiguration _configuration;

        public OrderDetailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GetAll
        public List<OrderDetailModel> GetAllOrderDetails()
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");
            var orderDetails = new List<OrderDetailModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstr))
                {
                    SqlCommand cmd = new SqlCommand("PR_OrderDetail_SelectAll", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        orderDetails.Add(new OrderDetailModel
                        {
                            OrderDetailID = reader["OrderDetailID"] != DBNull.Value ? Convert.ToInt32(reader["OrderDetailID"]) : 0,
                            OrderID = reader["OrderID"] != DBNull.Value ? Convert.ToInt32(reader["OrderID"]) : 0,
                            ProductID = reader["ProductID"] != DBNull.Value ? reader["ProductID"].ToString() : string.Empty,
                            Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0,
                            Amount = reader["Amount"] != DBNull.Value ? Convert.ToDecimal(reader["Amount"]) : 0.0m,
                            TotalAmount = reader["TotalAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalAmount"]) : 0.0m,
                            UserID = reader["UserID"] != DBNull.Value ? Convert.ToInt32(reader["UserID"]) : 0
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching order details: " + ex.Message);
            }

            return orderDetails;
        }
        #endregion

        #region SelectByPk
        public OrderDetailModel SelectByPk(int orderDetailID)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");
            OrderDetailModel orderDetail = null;

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetail_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    orderDetail = new OrderDetailModel
                    {
                        OrderDetailID = Convert.ToInt32(reader["OrderDetailID"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        ProductID = reader["ProductID"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Amount = Convert.ToDecimal(reader["Amount"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
                }
            }
            return orderDetail;
        }
        #endregion

        #region Insert
        public bool Insert(OrderDetailModel orderDetail)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetail_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
                cmd.Parameters.AddWithValue("@ProductID", orderDetail.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                cmd.Parameters.AddWithValue("@Amount", orderDetail.Amount);
                cmd.Parameters.AddWithValue("@TotalAmount", orderDetail.TotalAmount);
                cmd.Parameters.AddWithValue("@UserID", orderDetail.UserID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(OrderDetailModel orderDetail)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetail_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@OrderDetailID", orderDetail.OrderDetailID);
                cmd.Parameters.AddWithValue("@OrderID", orderDetail.OrderID);
                cmd.Parameters.AddWithValue("@ProductID", orderDetail.ProductID);
                cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
                cmd.Parameters.AddWithValue("@Amount", orderDetail.Amount);
                cmd.Parameters.AddWithValue("@TotalAmount", orderDetail.TotalAmount);
                cmd.Parameters.AddWithValue("@UserID", orderDetail.UserID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int orderDetailID)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_OrderDetail_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region DropDown for Order
        public IEnumerable<OrderDropDownModel> GetOrder()
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");
            
            var orders = new List<OrderDropDownModel>();

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Orders_DropDown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    orders.Add(new OrderDropDownModel
                    {
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        OrderCode = reader["OrderCode"].ToString()
                    });
                }
            }
            return orders;
        }
        #endregion

        #region DropDown for Product
        public IEnumerable<ProductDropDownModel> GetProduct()
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            var products = new List<ProductDropDownModel>();

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Product_DropDown", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new ProductDropDownModel
                    {
                        ProductID = Convert.ToInt32(reader["ProductID"]),
                        ProductName = reader["ProductName"].ToString()
                    });
                }
            }
            return products;
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
