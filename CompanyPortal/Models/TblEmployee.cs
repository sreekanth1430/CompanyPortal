using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Models
{
    public class TblEmployee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [ForeignKey("EmployeesInRole")]
        public int RoleId { get; set; }
        public TblEmployeesInRole EmployeesInRole { get; set; }
    }
}
