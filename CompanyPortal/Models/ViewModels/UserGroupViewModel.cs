namespace CompanyPortal.Models.ViewModels
{
    public class UserGroupViewModel
    {
        public IEnumerable<TblUserGroup> UserGroups { get; set; }
        public int SelectedGroupId { get; set; }
        public List<TblUser> GroupUsers { get; set; }
        public List<TblUser> OtherUsers { get; set; }
    }
}
