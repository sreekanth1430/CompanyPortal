using CompanyPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyPortal.Data
{
    public class CompanyDBContext : DbContext
    {
        public CompanyDBContext(DbContextOptions<CompanyDBContext> options) : base(options)
        {
        }

        public DbSet<TblCompany> TblCompanies { get; set; }
        public DbSet<TblUser> TblUsers { get; set; }
        public DbSet<TblUserGroup> TblUserGroups { get; set; }
        public DbSet<UserGroupMapping> UserGroupMappings { get; set; }
        public DbSet<TblLink> TblLinks { get; set; }
        public DbSet<TblContact> TblContacts { get; set; }
        public DbSet<TblCompilenceDocument> TblCompilenceDocuments { get; set; }
        public DbSet<TblLocation> TblLocations { get; set; }
        public DbSet<TblEmployee> TblEmployees { get; set; }
        public DbSet<TblEmployeesInRole> TblEmployeesInRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCompany>().ToTable("TblCompany");
            modelBuilder.Entity<TblUser>().ToTable("TblUser");
            modelBuilder.Entity<TblUserGroup>().ToTable("TblUserGroup");
            modelBuilder.Entity<UserGroupMapping>().ToTable("UserGroupMapping"); 
            modelBuilder.Entity<TblLink>().ToTable("tblLinks");
            modelBuilder.Entity<TblContact>().ToTable("tblContacts");
            modelBuilder.Entity<TblCompilenceDocument>().ToTable("tblCompilenceDocuments");
            modelBuilder.Entity<TblLocation>().ToTable("TblLocation");
            modelBuilder.Entity<TblEmployee>().ToTable("TblEmployee");
            modelBuilder.Entity<TblEmployeesInRole>().ToTable("TblEmployeesInRole");

            modelBuilder.Entity<UserGroupMapping>()
                .HasKey(ugm => new { ugm.UserId, ugm.GroupId });

            modelBuilder.Entity<UserGroupMapping>()
                .HasOne(ugm => ugm.User)
                .WithMany(u => u.UserGroupMapping)
                .HasForeignKey(ugm => ugm.UserId);

            modelBuilder.Entity<UserGroupMapping>()
                .HasOne(ugm => ugm.UserGroup)
                .WithMany(ug => ug.UserGroups)
                .HasForeignKey(ugm => ugm.GroupId);
        }
        public DbSet<CompanyPortal.Models.TblUrgencyLevel> TblUrgencyLevel { get; set; } = default!;
        public DbSet<CompanyPortal.Models.TblRequestType> TblRequestType { get; set; } = default!;
        public DbSet<CompanyPortal.Models.TblTicketAssignment> TblTicketAssignment { get; set; } = default!;
    }
}
