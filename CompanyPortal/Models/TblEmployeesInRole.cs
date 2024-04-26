using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Models
{
    public class TblEmployeesInRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public int RoleName { get; set; }
        public int CompanyId { get; set; }
        public TblCompany Company { get; set; }
    }
}
