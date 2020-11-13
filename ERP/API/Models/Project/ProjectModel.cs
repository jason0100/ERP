using API.Models.Expenditure;
using API.Models.Partner;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Project
{
    public class ProjectModel
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required]
        [Column("master_partner_id", TypeName = "int")]
        public int master_partner_id { get; set; }

        /// <summary>
        /// 專案名稱
        /// </summary>
        [Required]
        [Column("name", TypeName = "nvarchar(55)")]
        public string name { get; set; }

        /// <summary>
        /// 預算
        /// </summary>
        [Required]
        [Column("budget", TypeName = "decimal(18, 2)")]
        public decimal? budget { get; set; }

        /// <summary>
        /// 專案總期程 開始日 2020-12-31
        /// </summary>
        [Required]
        [Column("date_start", TypeName = "date")]
        public DateTime? date_start { get; set; }

        /// <summary>
        /// 專案總期程 結束日 2020-12-31
        /// </summary>
        [Required]
        [Column("date_end", TypeName = "date")]
        public DateTime? date_end { get; set; }

        /// <summary>
        /// 內部人力 (人)
        /// </summary>
        [Column("internal_HR", TypeName = "int")]
        public int? internal_HR { get; set; }

        /// <summary>
        /// 委外人力 (人)
        /// </summary>
        [Column("outsourcing_HR", TypeName = "int")]
        public int? outsourcing_HR { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [Column("remarks", TypeName = "nvarchar(MAX)")]
        public string remarks { get; set; }

        [NotMapped]
        public string partner_id { get; set; }//相較ProjectModel增加的欄位


        /// <summary>
        /// 專案狀態 (參考misc table id=5 data)
        /// </summary>
        [Required]
        [Column("status", TypeName = "tinyint")]
        public byte? status { get; set; }

        [Required]
        [Column("created", TypeName = "datetime")]
        public DateTime created { get; set; }

        [Column("updated", TypeName = "datetime")]
        public DateTime? updated { get; set; }

       

    }
}
