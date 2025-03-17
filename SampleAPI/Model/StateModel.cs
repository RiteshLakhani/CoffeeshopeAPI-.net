using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Model
{
    public class StateModel
    {
        public int? StateID { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public int CountryID { get; set; }
    }
}
