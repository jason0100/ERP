
using API.Models.Admin;
using API.Models.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.User
{
    public class User
    {
        public int id { get; set; }
      
        public string user_uuid { get; set; }
        public string name { get; set; }
        public string national_id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? birthdate { get; set; }

        public int? city_id { get; set; }
        public int? district_id { get; set; }
        public int? village_id { get; set; }

        public string address { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string mobile { get; set; }
     
        public int? status { get; set; }

        [DataType(DataType.Date)]
   
                public DateTime created { get; set; }
        public DateTime? updated { get; set; }

        
    }
}
