using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class TeacherSubjectAcademicMappingMap : EntityTypeConfiguration<TeacherSubjectAcademicMapping>
    {
        public TeacherSubjectAcademicMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Description)
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("TeacherSubjectAcademicMappings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicShiftId).HasColumnName("AcademicShiftId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.AcademicSectionId).HasColumnName("AcademicSectionId");
            this.Property(t => t.SubjectId).HasColumnName("SubjectId");
            this.Property(t => t.SubjectSplitId).HasColumnName("SubjectSplitId");
            this.Property(t => t.TeacherId).HasColumnName("TeacherId");
            this.Property(t => t.SubjectGroupId).HasColumnName("SubjectGroupId");
            this.Property(t => t.Description).HasColumnName("Description");

            // Relationships
            this.HasOptional(t => t.AcademicBranch)
                .WithMany(t => t.TeacherSubjectAcademicMappings)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasOptional(t => t.AcademicClass)
                .WithMany(t => t.TeacherSubjectAcademicMappings)
                .HasForeignKey(d => d.AcademicClassId);
            this.HasOptional(t => t.AcademicSection)
                .WithMany(t => t.TeacherSubjectAcademicMappings)
                .HasForeignKey(d => d.AcademicSectionId);
            this.HasOptional(t => t.AcademicShift)
                .WithMany(t => t.TeacherSubjectAcademicMappings)
                .HasForeignKey(d => d.AcademicShiftId);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.TeacherSubjectAcademicMappings)
                .HasForeignKey(d => d.TeacherId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.TeacherSubjectAcademicMappings)
                .HasForeignKey(d => d.InstituteId);
            this.HasOptional(t => t.Subject)
                .WithMany(t => t.TeacherSubjectAcademicMappings)
                .HasForeignKey(d => d.SubjectId);

        }
    }
}
