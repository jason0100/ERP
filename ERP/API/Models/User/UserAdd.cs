using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.User
{
    public class UserAdd
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string national_id { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string district { get; set; }
        [Required]
        public string village { get; set; }
        [Required]
        public string address { get; set; }
        [DataType(DataType.Date)]
        public DateTime birthdate { get; set; }
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string mobile { get; set; }
    }
}
