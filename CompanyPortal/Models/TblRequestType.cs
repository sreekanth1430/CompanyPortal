using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class TblRequestType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestTypeId { get; set; }
        public string RequestTypeName { get; set; }
        public int CompanyId { get; set; }
        public TblCompany Company { get; set; }
    }
}
