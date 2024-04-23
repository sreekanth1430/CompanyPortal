using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyPortal.Models
{
    public class TblContact
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }
        public bool? DisplayEmployee { get; set; }
        public DateTime? Insert_date { get; set; }
        public int? ContactOrder { get; set; }

        public TblCompany Company { get; set; }
    }
}
