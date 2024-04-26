namespace CompanyPortal.Models.ViewModels
{
    public class EmployeeRoleViewModel
    {
        public List<TblEmployee> EmployeeUsers { get; set; }
        public List<TblEmployee> HrUsers { get; set; }
        public List<TblEmployee> CampusUsers { get; set; }
    }
}
