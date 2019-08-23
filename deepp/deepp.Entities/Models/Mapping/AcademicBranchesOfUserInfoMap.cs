using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class AcademicBranchesOfUserInfoMap : EntityTypeConfiguration<AcademicBranchesOfUserInfo>
    {
        public AcademicBranchesOfUserInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("AcademicBranchesOfUserInfoes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserInfoId).HasColumnName("UserInfoId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.AcademicBranchesOfUserInfoes)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.UserInfo)
                .WithMany(t => t.AcademicBranchesOfUserInfoes)
                .HasForeignKey(d => d.UserInfoId);

        }
    }
}
