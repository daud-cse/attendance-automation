using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class UserAttendanceDetailMap : EntityTypeConfiguration<UserAttendanceDetail>
    {
        public UserAttendanceDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Comments)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("UserAttendanceDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserAttendanceId).HasColumnName("UserAttendanceId");
            this.Property(t => t.UserInfoId).HasColumnName("UserInfoId");
            this.Property(t => t.AttendanceTypeId).HasColumnName("AttendanceTypeId");
            this.Property(t => t.InTime).HasColumnName("InTime");
            this.Property(t => t.OutTime).HasColumnName("OutTime");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.AttendanceType)
                .WithMany(t => t.UserAttendanceDetails)
                .HasForeignKey(d => d.AttendanceTypeId);
            this.HasRequired(t => t.UserAttendance)
                .WithMany(t => t.UserAttendanceDetails)
                .HasForeignKey(d => d.UserAttendanceId);
            this.HasRequired(t => t.UserInfo)
                .WithMany(t => t.UserAttendanceDetails)
                .HasForeignKey(d => d.UserInfoId);

        }
    }
}
