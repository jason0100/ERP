using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Users
{
    public class payload
    {
        public string uuid { get; set; }
        public string unique_name { get; set; }
        public string exp { get; set; }
        public int? type { get; set; }
    }
}
