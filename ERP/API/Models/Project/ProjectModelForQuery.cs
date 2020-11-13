using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Project
{
    public class ProjectModelForQuery
    {
        /// <summary>
        /// 專案id
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// 查詢起始日
        /// </summary>
        public DateTime? date_start { get; set; }
        /// <summary>
        /// 查詢結束日
        /// </summary>
        public DateTime? date_end { get; set; }
        /// <summary>
        /// 查詢單位(合作廠商)
        /// </summary>
        public int? partner_id { get; set; }

       
    }
}
