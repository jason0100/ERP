using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Item
{
    public class ItemModel
    {
        [Key]
        [Column("id", TypeName="int")]
        public int id { get; set; }

        [Required]
        [Column("name", TypeName = "nvarchar(50)")]
        public string name { get; set; }

        [Required]
        [Column("created", TypeName = "datetime")]
        public DateTime? created { get; set; }

        [Column("updated", TypeName = "datetime")]
        public DateTime? updated { get; set; }
    }
}
