using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class StudentAttendanceMap : EntityTypeConfiguration<StudentAttendance>
    {
        public StudentAttendanceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("StudentAttendances");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AttendanceDate).HasColumnName("AttendanceDate");
            this.Property(t => t.TeacherId).HasColumnName("TeacherId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.AcademicShiftId).HasColumnName("AcademicShiftId");
            this.Property(t => t.AcademicSectionId).HasColumnName("AcademicSectionId");
            this.Property(t => t.AcamedicGroupId).HasColumnName("AcamedicGroupId");
            this.Property(t => t.SubjectAcademicClassMappingsId).HasColumnName("SubjectAcademicClassMappingsId");
            this.Property(t => t.AcademicPeriodId).HasColumnName("AcademicPeriodId");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.SyncDate).HasColumnName("SyncDate");
            this.Property(t => t.PresentCount).HasColumnName("PresentCount");
            this.Property(t => t.AbsentCount).HasColumnName("AbsentCount");
            this.Property(t => t.TotalCount).HasColumnName("TotalCount");
            this.Property(t => t.AbscondingCount).HasColumnName("AbscondingCount");
            this.Property(t => t.PresentPercentage).HasColumnName("PresentPercentage");
            this.Property(t => t.AbsentPercentage).HasColumnName("AbsentPercentage");
            this.Property(t => t.AbscondingPercentage).HasColumnName("AbscondingPercentage");
            this.Property(t => t.LocalId).HasColumnName("LocalId");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.StudentAttendances)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.AcademicClass)
                .WithMany(t => t.StudentAttendances)
                .HasForeignKey(d => d.AcademicClassId);
            this.HasOptional(t => t.AcademicClassSectionMapping)
                .WithMany(t => t.StudentAttendances)
                .HasForeignKey(d => d.AcademicSectionId);
            this.HasOptional(t => t.AcademicGroup)
                .WithMany(t => t.StudentAttendances)
                .HasForeignKey(d => d.AcamedicGroupId);
            this.HasOptional(t => t.AcademicPeriod)
                .WithMany(t => t.StudentAttendances)
                .HasForeignKey(d => d.AcademicPeriodId);
            this.HasRequired(t => t.AcademicSession)
                .WithMany(t => t.StudentAttendances)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasOptional(t => t.AcademicShift)
                .WithMany(t => t.StudentAttendances)
                .HasForeignKey(d => d.AcademicShiftId);
            this.HasOptional(t => t.SubjectAcademicClassMapping)
                .WithMany(t => t.StudentAttendances)
                .HasForeignKey(d => d.SubjectAcademicClassMappingsId);
            this.HasRequired(t => t.Teacher)
                .WithMany(t => t.StudentAttendances)
                .HasForeignKey(d => d.TeacherId);

        }
    }
}
