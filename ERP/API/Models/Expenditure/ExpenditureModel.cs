using API.Models.Partner;
using API.Models.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Models.Expenditure
{
    public class ExpenditureModel
    {

        [Key]
        [Column("id", TypeName = "int")]
        public int? id { get; set; }
        /// <summary>
        /// 專案id
        /// </summary>
        [Required]
        [Column("project_id", TypeName = "int")]
        public int? project_id { get; set; }


        /// <summary>
        /// 單位名稱(合作廠商 ) id
        /// </summary>
        [Required]
        [Column("partner_id", TypeName = "int")]
        public int? partner_id { get; set; }


        /// <summary>
        /// 項目id
        /// </summary>
        [Required]
        [Column("item_id", TypeName = "int")]
        public int? item_id { get; set; }


        /// <summary>
        /// 應付日期
        /// </summary>
        [Required]
        [Column("due_date", TypeName = "datetime")]
        public DateTime? due_date { get; set; }


        /// <summary>
        /// 實付日期
        /// </summary>
        [Column("payment_date", TypeName = "datetime")]
        public DateTime? payment_date { get; set; }


        /// <summary>
        /// 應付金額元
        /// </summary>
        [Required]
        [Column("amount_payable", TypeName = "decimal(18,2)")]
        public decimal? amount_payable { get; set; }


        /// <summary>
        /// 實付金額元
        /// </summary>
        [Column("amount_paid", TypeName = "decimal(18,2)")]
        public decimal? amount_paid { get; set; }


        /// <summary>
        /// 資料狀態0=disable, 1=enable
        /// </summary>
        [Required]
        [Range(0, 1)]
        [Column("status", TypeName = "tinyint")]
        public byte? status { get; set; }



        [Required]
        [Column("created", TypeName = "datetime")]
        public DateTime created { get; set; }


        [Column("updated", TypeName = "datetime")]
        public DateTime? updated { get; set; }

        [ForeignKey("partner_id")]
        public virtual PartnerModel partner_ { get; set; }

        [ForeignKey("project_id")]
        public virtual ProjectModel project_ { get; set; }
    }
}
