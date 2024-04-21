using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Models
{
    public class TblUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }
        public TblCompany Company { get; set; }
        public ICollection<UserGroupMapping> UserGroupMapping { get; set; }
    }
}
