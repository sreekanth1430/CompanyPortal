using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class TblLocation
    {
        [Key]
        public int LocationID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LocationCode { get; set; }
        public int CompanyId { get; set; }

        // Navigation property for the Company
        public TblCompany Company { get; set; }
    }
}
