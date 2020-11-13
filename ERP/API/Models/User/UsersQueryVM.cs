using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.User
{
    public class UsersQueryVM
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public bool? IsActivated { get; set; }
        public string RoleName { get; set; }


        public int Row { get; set; }
        public int Page { get; set; }
    }
}
