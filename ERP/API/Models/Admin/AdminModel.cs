
using API.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Admin
{
    public class AdminModel
    {
        public int id { get; set; }

        public string username { get; set; }
      
        public string password { get; set; }
        public DateTime password_expiration { get; set; }
        public string national_id { get; set; }
        public string name { get; set; }
        public string organization { get; set; }
        public string mobile { get; set; }

        public string admin_uuid { get; set; }

        public int card_id { get; set; }
        
        public int role_id { get; set; }
        public int status { get; set; }
        public DateTime created { get; set; }
        public DateTime? updated { get; set; }

        public string challenge { get; set; }
        public byte[] certificate_login { get; set; }
        public int? type { get; set; }//1=eid-server admin, 2=MOI web admin, 3=DCA web admin , 4=household app system admin
        public Role role_ { get; set; }

    }
}
