using Microsoft.AspNetCore.Mvc.Rendering;

namespace CompanyPortal.Models.ViewModels
{
    public class TicketAssignmentViewModel
    {
        public int SelectedGroupId { get; set; }
        public int SelectedUrgencyLevelId { get; set; }
        public int SelectedRequestTypeId { get; set; }
        public int SelectedUserId { get; set; }
        public IEnumerable<SelectListItem> Groups { get; set; }
        public IEnumerable<SelectListItem> UrgencyLevels { get; set; }
        public IEnumerable<SelectListItem> RequestTypes { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }
        public IEnumerable<TblTicketAssignment> TicketAssignments { get; set; }
    }
}
