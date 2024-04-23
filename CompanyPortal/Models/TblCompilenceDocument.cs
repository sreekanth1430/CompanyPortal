using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyPortal.Models
{
    public class TblCompilenceDocument
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string DocumentAlias { get; set; }
        public string DocumentAliasLink { get; set; }
        public int CompanyId { get; set; }
        public DateTime? InsertDate { get; set; }
        public string Title { get; set; }
        public int? DocumentOrder { get; set; }

        public TblCompany Company { get; set; }
    }
}
