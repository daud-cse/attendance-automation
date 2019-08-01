using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class LeaveApplicationMap : EntityTypeConfiguration<LeaveApplication>
    {
        public LeaveApplicationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("LeaveApplications");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.UserInfoId).HasColumnName("UserInfoId");
            this.Property(t => t.AttendanceTypeId).HasColumnName("AttendanceTypeId");
            this.Property(t => t.ApplyDate).HasColumnName("ApplyDate");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.IsHalfDay).HasColumnName("IsHalfDay");
            this.Property(t => t.IsFirstHalf).HasColumnName("IsFirstHalf");
            this.Property(t => t.IsSecondHalf).HasColumnName("IsSecondHalf");
            this.Property(t => t.LeaveCount).HasColumnName("LeaveCount");
            this.Property(t => t.IsApproved).HasColumnName("IsApproved");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.LeaveApplications)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.AttendanceType)
                .WithMany(t => t.LeaveApplications)
                .HasForeignKey(d => d.AttendanceTypeId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.LeaveApplications)
                .HasForeignKey(d => d.InstituteId);
            this.HasRequired(t => t.UserInfo)
                .WithMany(t => t.LeaveApplications)
                .HasForeignKey(d => d.UserInfoId);

        }
    }
}
