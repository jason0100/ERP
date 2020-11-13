using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.User
{
    public class UserEidPut
    {
        [Required]
        public string national_id { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string mobile { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        public int send_SMS_code { get; set; }
        public int send_email_code { get; set; }
    }
}
