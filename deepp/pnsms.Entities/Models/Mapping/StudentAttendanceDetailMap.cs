using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class StudentAttendanceDetailMap : EntityTypeConfiguration<StudentAttendanceDetail>
    {
        public StudentAttendanceDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Comments)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("StudentAttendanceDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.StudentAttendanceId).HasColumnName("StudentAttendanceId");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.AttendanceTypeId).HasColumnName("AttendanceTypeId");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.IsAbsconding).HasColumnName("IsAbsconding");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.LocalId).HasColumnName("LocalId");

            // Relationships
            this.HasRequired(t => t.AttendanceType)
                .WithMany(t => t.StudentAttendanceDetails)
                .HasForeignKey(d => d.AttendanceTypeId);
            this.HasRequired(t => t.StudentAttendance)
                .WithMany(t => t.StudentAttendanceDetails)
                .HasForeignKey(d => d.StudentAttendanceId);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.StudentAttendanceDetails)
                .HasForeignKey(d => d.StudentId);

        }
    }
}
