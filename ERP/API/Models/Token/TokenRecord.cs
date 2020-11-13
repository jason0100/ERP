using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Token
{
    public class TokenRecord
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public string jwt_token { get; set; }
        public int status { get; set; }// 0 = disable, 1= active
        public DateTime created { get; set; }
        public DateTime? updated { get; set; }
    }
}
