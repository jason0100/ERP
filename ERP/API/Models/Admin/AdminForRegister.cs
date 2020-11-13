using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
#pragma warning disable 1591
namespace API.Models.Admin
{
    public class AdminForRegister:IValidatableObject
    {
        public int id { get; set; }

        [Required]
        [MaxLength(20)]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public string passwordHash { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string organization { get; set; }
        [Required]
        public string mobile { get; set; }

        [Required]
        public int? card_id { get; set; }

        [Required]
        public int? role_id { get; set; }
        [Required]
        public string national_id { get; set; }
        [Required]
        public int? type { get; set; }//1=管理平台eid-server admin, 2=內政部本部MOI web admin, 3=直轄縣市=民政局=DCADCA web admin , 4=戶役政household app system admin,999=superAdmin


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int[] typeArr = new int[] { 1, 2, 3, 4, 999 };
            if (type != null)
            {
                var isExist = Array.IndexOf(typeArr,Convert.ToInt32(type));
                if (isExist == -1)
                    yield return new ValidationResult("type value shold b one of [1,2,3,4,999]", new string[] { "type" });
            }
        }
    }
}
