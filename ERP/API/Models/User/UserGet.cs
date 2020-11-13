using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace API.Models.User
{
    public class UserGet
    {
        public string national_id { get; set; }
        public int card_id { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string village { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter positive valid integer Number")]
        public int? page_number { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter positive valid integer Number")]
        public int? page_sizes { get; set; }

        public int? card_status { get; set; }
        public bool? received_card { get; set; }
        public string? email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string? mobile { get; set; }
        public string name { get; set; }

        public string arrange_for { get; set; }
        public string arrange { get; set; }
    }
}
