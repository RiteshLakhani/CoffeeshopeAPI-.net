using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SampleAPI.Model
{
    public class OrderDetailModel
    {
        public int? OrderDetailID { get; set; }
        public int? OrderID { get; set; }
        public string ProductID { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? UserID { get; set; }
    }

}
