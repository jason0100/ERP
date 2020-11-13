using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Roles
{
    public class ListRole
    {
        public int id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter positive valid integer Number")]
        public int? page_number { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter positive valid integer Number")]
        public int? page_sizes { get; set; }
        public string arrange_for { get; set; }
        public string arrange { get; set; }
    }
}
