using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Roles
{
    public class CreateRole
    {
        [Required]
        public string name { get; set; }
    }
}
