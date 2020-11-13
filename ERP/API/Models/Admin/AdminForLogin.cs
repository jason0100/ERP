//using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Admin
{
#pragma warning disable 1591
    public class AdminForLogin : IValidatableObject
    {

        public int? card_id { get; set; }
        /// <summary>
        /// 使用者名稱
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 使用者密碼
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 簽章
        /// </summary>
        public string signature { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (!string.IsNullOrEmpty(username))
            {
                if (string.IsNullOrEmpty(password))
                    yield return new ValidationResult("Password field is required.", new string[] { "password" });
            }


        }
    }

}
