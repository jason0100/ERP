using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.User
{
    public class UserGetResult
    {
        public int id { get; set; }
        public int card_id { get; set; }
        public string name { get; set; }
        public string national_id { get; set; }
        public DateTime birthdate { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string village { get; set; }
        public string address { get; set; }
        public int status { get; set; }
    }
}
