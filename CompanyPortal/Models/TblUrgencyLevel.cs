using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class TblUrgencyLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UrgencyLevelId { get; set; }
        public string UrgencyLevelName { get; set; }
        public int UrgencyOrder { get; set; }
        public int CompanyId { get; set; }
        public TblCompany Company { get; set; }
    }
}
