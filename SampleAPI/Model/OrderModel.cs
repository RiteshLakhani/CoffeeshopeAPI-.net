using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SampleAPI.Model
{
    public class OrderModel
    {
        public int? OrderID { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerID { get; set; }
        public string PaymentMode { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public int UserID { get; set; }
    }

    public class OrderDropDownModel
    {
        public int OrderID { get; set; }
        public string OrderCode { get; set; }
    }
}
