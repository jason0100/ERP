using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Admin
{
    public class AdminCard
    {
        public int id { get; set; }
        public int admin_id { get; set; }

        public byte[] pk { get; set; }
        public byte[] puk { get; set; }
    }
}
