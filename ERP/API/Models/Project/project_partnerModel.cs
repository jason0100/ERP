using API.Models.Partner;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System;

namespace API.Models.Project
{
    public class project_partnerModel
    {
        
        [Key]
        public int id { get; set; }

        [Column("partner_id", TypeName = "int")]
        public int partner_id { get; set; }

        [Column("project_id", TypeName = "int")]
        public int project_id { get; set; }

        [Required]
        [Column("created", TypeName = "datetime")]
        public DateTime created { get; set; }

        [Column("updated", TypeName = "datetime")]
        public DateTime? updated { get; set; }

       
        public virtual PartnerModel partner_ { get; set; }
       
        public virtual ProjectModel project_ { get; set; }
    }
}
