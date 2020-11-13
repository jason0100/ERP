using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Partner
{
    public class PartnerModel
    {
        public int id { get; set; }
        public int? organization_id { get; set; }
        public string email { get; set; }
        public string landline { get; set; }
        public string address { get; set; }
        public int? tax_ID_number { get; set; }
        public string contact_number { get; set; }
        public string  contact_person { get; set; }
        public decimal? capital { get; set; }
        public string representative { get; set; }
        public string website { get; set; }
        public byte? status { get; set; }
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
    }
}
