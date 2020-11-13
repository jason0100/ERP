//using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace API.Models.Admin
{
    public class AdminForQuery:IValidatableObject
    {
        /// <summary>
        /// 以admin_id搜尋
        /// </summary>
        public int admin_id { get; set; }
        public string admin_uuid { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string national_id { get; set; }
        
        public string organization { get; set; }
        public string mobile { get; set; }
        public int? status { get; set; }
        public int role_id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter positive valid integer Number")]
        public int? page_number { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter positive valid integer Number")]
        public int? page_sizes { get; set; }
        public string arrange_for { get; set; }
        public string arrange { get; set; }

        [DataType(DataType.Date)]
        public DateTime? password_expiration_date_end { get; set; }
        [DataType(DataType.Date)]
        public DateTime? password_expiration_date_init { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (password_expiration_date_end == null ^ password_expiration_date_init == null)
                yield return new ValidationResult("Both fileds of password_expiration_date_end and password_expiration_date_init are required.");
            if (password_expiration_date_end <= password_expiration_date_init )
                yield return new ValidationResult("Field password_expiration_date_end should later than password_expiration_date_init.");
        }
    }

  
}
