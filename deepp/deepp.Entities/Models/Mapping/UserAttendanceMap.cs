using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class UserAttendanceMap : EntityTypeConfiguration<UserAttendance>
    {
        public UserAttendanceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("UserAttendances");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.UserInfoTypeId).HasColumnName("UserInfoTypeId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AttendanceDate).HasColumnName("AttendanceDate");
            this.Property(t => t.PresentCount).HasColumnName("PresentCount");
            this.Property(t => t.AbsentCount).HasColumnName("AbsentCount");
            this.Property(t => t.TotalCount).HasColumnName("TotalCount");
            this.Property(t => t.PresentPercentage).HasColumnName("PresentPercentage");
            this.Property(t => t.AbsentPercentage).HasColumnName("AbsentPercentage");
            this.Property(t => t.LastAttendanceSynDate).HasColumnName("LastAttendanceSynDate");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.UserAttendances)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.UserInfoType)
                .WithMany(t => t.UserAttendances)
                .HasForeignKey(d => d.UserInfoTypeId);

        }
    }
}
