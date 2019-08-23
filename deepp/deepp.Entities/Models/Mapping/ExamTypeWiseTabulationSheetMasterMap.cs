using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ExamTypeWiseTabulationSheetMasterMap : EntityTypeConfiguration<ExamTypeWiseTabulationSheetMaster>
    {
        public ExamTypeWiseTabulationSheetMasterMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.AcademicClassName)
                .HasMaxLength(256);

            this.Property(t => t.AcademicGroupName)
                .HasMaxLength(256);

            this.Property(t => t.AcademicSectionName)
                .HasMaxLength(256);

            this.Property(t => t.AcademicSessionName)
                .HasMaxLength(256);

            this.Property(t => t.StudentName)
                .HasMaxLength(128);

            this.Property(t => t.ExamGradeName)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ExamTypeWiseTabulationSheetMaster");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.ExamTypeId).HasColumnName("ExamTypeId");
            this.Property(t => t.AcademicClassesId).HasColumnName("AcademicClassesId");
            this.Property(t => t.AcademicClassName).HasColumnName("AcademicClassName");
            this.Property(t => t.SubjectAcademicClassMappingsMapId).HasColumnName("SubjectAcademicClassMappingsMapId");
            this.Property(t => t.AcademicGroupId).HasColumnName("AcademicGroupId");
            this.Property(t => t.AcademicGroupName).HasColumnName("AcademicGroupName");
            this.Property(t => t.AcademicClassesSectionMapId).HasColumnName("AcademicClassesSectionMapId");
            this.Property(t => t.AcademicSectionId).HasColumnName("AcademicSectionId");
            this.Property(t => t.AcademicSectionName).HasColumnName("AcademicSectionName");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.AcademicSessionName).HasColumnName("AcademicSessionName");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.StudentName).HasColumnName("StudentName");
            this.Property(t => t.TotalMarks).HasColumnName("TotalMarks");
            this.Property(t => t.TotalSubject).HasColumnName("TotalSubject");
            this.Property(t => t.AverageNumber).HasColumnName("AverageNumber");
            this.Property(t => t.ExamGradeId).HasColumnName("ExamGradeId");
            this.Property(t => t.ExamGradeName).HasColumnName("ExamGradeName");
            this.Property(t => t.ExamGradePoint).HasColumnName("ExamGradePoint");
            this.Property(t => t.LastUpdateDate).HasColumnName("LastUpdateDate");

            // Relationships
            this.HasOptional(t => t.ExamType)
                .WithMany(t => t.ExamTypeWiseTabulationSheetMasters)
                .HasForeignKey(d => d.ExamTypeId);

        }
    }
}
