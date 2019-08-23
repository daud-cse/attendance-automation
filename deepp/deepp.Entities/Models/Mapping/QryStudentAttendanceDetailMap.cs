using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class QryStudentAttendanceDetailMap : EntityTypeConfiguration<QryStudentAttendanceDetail>
    {
        public QryStudentAttendanceDetailMap()
        {
            // Primary Key
            this.HasKey(t => new { t.StudentAttendanceId, t.StudentId, t.AttendanceTypeId });

            // Properties
            this.Property(t => t.StudentAttendanceId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.StudentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.AttendanceTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Flag)
                .HasMaxLength(2);

            // Table & Column Mappings
            this.ToTable("QryStudentAttendanceDetails");
            this.Property(t => t.StudentAttendanceId).HasColumnName("StudentAttendanceId");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.AttendanceTypeId).HasColumnName("AttendanceTypeId");
            this.Property(t => t.IsAbsconding).HasColumnName("IsAbsconding");
            this.Property(t => t.Flag).HasColumnName("Flag");
            this.Property(t => t.IsPresent).HasColumnName("IsPresent");
            this.Property(t => t.AttendanceDate).HasColumnName("AttendanceDate");
        }
    }
}
