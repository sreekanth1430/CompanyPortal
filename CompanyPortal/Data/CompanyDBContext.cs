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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCompany>().ToTable("TblCompany");
            modelBuilder.Entity<TblUser>().ToTable("TblUser");
            modelBuilder.Entity<TblUserGroup>().ToTable("TblUserGroup");
            modelBuilder.Entity<UserGroupMapping>().ToTable("UserGroupMapping");

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
    }
}
