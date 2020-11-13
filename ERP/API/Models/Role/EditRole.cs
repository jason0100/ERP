using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Roles
{
    public class EditRole
    {
        [Required]
        public int id { get; set; }
        public string name { get; set; }

    }
}
