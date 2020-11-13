//using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace API.Models.User
{
    public class UserForLogin
    {
        /// <summary>
        /// 身分證字號
        /// </summary>
        public string national_id { get; set; }
        //目前沒有card暫停此功能
        public int card_id { get; set; }

        
        
    }

}
