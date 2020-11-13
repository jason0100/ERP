using API.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class LoginHuman
    {
        
        public string uuid { get; set; }
      public int type { get; set; }
        public TokenModel token { get; set; }
    }
}
