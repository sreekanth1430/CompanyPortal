using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.Models
{
    public class TblTicketAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketAssignmentId { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public string UrgencyLevelName { get; set; }
        public int UrgencyLevelId { get; set; }
        public string RequestTypeName { get; set; }
        public int RequestTypeId { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
    }
}
