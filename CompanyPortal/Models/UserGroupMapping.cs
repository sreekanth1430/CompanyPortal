namespace CompanyPortal.Models
{
    public class UserGroupMapping
    {
        public int UserId { get; set; }
        public TblUser User { get; set; }
        public int GroupId { get; set; }
        public TblUserGroup UserGroup { get; set; }
    }
}
