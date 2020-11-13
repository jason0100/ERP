using API.Models.Token;
using API.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Admin
{
    public class LoginAdmin
    {
        //public int Id { get; set; }        //public int Id { get; set; }

        public string admin_uuid { get; set; }
      
        public TokenModel token { get; set; }
    }
}
