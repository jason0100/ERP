using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Token
{
    public class TokenModel
    {
        
        public string token { get; set; }
      
        ////幾秒過期
        //public int expires_in { get; set; }
    }
}
