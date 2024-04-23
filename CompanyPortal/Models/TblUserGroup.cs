using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPortal.Models
{
    public class TblUserGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int CompanyId { get; set; }
        public TblCompany Company { get; set; }
        public ICollection<UserGroupMapping> UserGroups { get; set; }
    }
}
