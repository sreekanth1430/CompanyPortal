using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyPortal.Models
{
    public class TblLink
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime? Insert_date { get; set; }
        public int? LinkOrder { get; set; }

        public TblCompany Company { get; set; }
    }
}
