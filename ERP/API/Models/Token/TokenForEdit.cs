using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Token
{
    public class TokenForEdit
    {
        public int? id { get; set; }
        public string uuid { get; set; }
        [Required]
        [Range(0,1)]
        public int? status { get; set; }// 0 = disable, 1= active

    }
}
