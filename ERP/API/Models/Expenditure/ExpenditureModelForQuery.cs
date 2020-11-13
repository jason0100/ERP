using API.Models.Partner;
using API.Models.Project;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Expenditure
{
    public class ExpenditureModelForQuery:IValidatableObject
    {
       
     
        /// <summary>
        /// 初始日期
        /// </summary>
        public DateTime? date_start { get; set; }
        /// <summary>
        /// 結束日期
        /// </summary>
        public DateTime? date_end { get; set; }

        /// <summary>
        /// 項目id
        /// </summary>
        public int? item_id { get; set; }
        /// <summary>
        /// 資料狀態0=disable, 1=enable
        /// </summary>
        [Range(0,1)]
        public byte? status { get; set; }
        /// <summary>
        /// 專案id
        /// </summary>
        public int? project_id { get; set; }
        /// <summary>
        /// 單位名稱(合作廠商 ) id
        /// </summary>
        public int? partner_id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (date_start != null && date_end != null)
            {
                if (date_end < date_start)
                    yield return new ValidationResult("結束日期不可早於開始日期",new string[] { "date_end"});
            }
        }
    }
}
