using SampleAPI.Model;
using System.Data;
using System.Data.SqlClient;
using SampleAPI.Data;

namespace SampleAPI.Data
{
    public class BillRepository
    {
        private readonly IConfiguration _configuration;

        public BillRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GetAll
        public List<BillModel> GetAllBills()
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");
            var bills = new List<BillModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionstr))
                {
                    SqlCommand cmd = new SqlCommand("PR_Bills_SelectAll", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        bills.Add(new BillModel
                        {
                            BillID = Convert.ToInt32(reader["BillID"]),
                            BillNumber = reader["BillNumber"].ToString(),
                            BillDate = Convert.ToDateTime(reader["BillDate"]),
                            OrderID = Convert.ToInt32(reader["OrderID"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            Discount = Convert.ToDecimal(reader["Discount"]),
                            NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                            UserID = Convert.ToInt32(reader["UserID"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching bills: " + ex.Message);
            }

            return bills;
        }
        #endregion

        #region SelectByPk
        public BillModel SelectByPk(int billID)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");
            BillModel bill = null;

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BillID", billID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    bill = new BillModel
                    {
                        BillID = Convert.ToInt32(reader["BillID"]),
                        BillNumber = reader["BillNumber"].ToString(),
                        BillDate = Convert.ToDateTime(reader["BillDate"]),
                        OrderID = Convert.ToInt32(reader["OrderID"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        Discount = Convert.ToDecimal(reader["Discount"]),
                        NetAmount = Convert.ToDecimal(reader["NetAmount"]),
                        UserID = Convert.ToInt32(reader["UserID"])
                    };
                }
            }
            return bill;
        }
        #endregion

        #region Insert
        public bool Insert(BillModel bill)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_Insert", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BillNumber", bill.BillNumber);
                cmd.Parameters.AddWithValue("@BillDate", bill.BillDate);
                cmd.Parameters.AddWithValue("@OrderID", bill.OrderID);
                cmd.Parameters.AddWithValue("@TotalAmount", bill.TotalAmount);
                cmd.Parameters.AddWithValue("@Discount", bill.Discount);
                cmd.Parameters.AddWithValue("@NetAmount", bill.NetAmount);
                cmd.Parameters.AddWithValue("@UserID", bill.UserID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Update
        public bool Update(BillModel bill)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_UpdateByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BillID", bill.BillID);
                cmd.Parameters.AddWithValue("@BillNumber", bill.BillNumber);
                cmd.Parameters.AddWithValue("@BillDate", bill.BillDate);
                cmd.Parameters.AddWithValue("@OrderID", bill.OrderID);
                cmd.Parameters.AddWithValue("@TotalAmount", bill.TotalAmount);
                cmd.Parameters.AddWithValue("@Discount", bill.Discount);
                cmd.Parameters.AddWithValue("@NetAmount", bill.NetAmount);
                cmd.Parameters.AddWithValue("@UserID", bill.UserID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region Delete
        public bool Delete(int billID)
        {
            string connectionstr = _configuration.GetConnectionString("ConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionstr))
            {
                SqlCommand cmd = new SqlCommand("PR_Bills_DeleteByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@BillID", billID);

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
