using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class ExamTypeWiseTabulationSheetDetailMap : EntityTypeConfiguration<ExamTypeWiseTabulationSheetDetail>
    {
        public ExamTypeWiseTabulationSheetDetailMap()
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

            this.Property(t => t.InstituteSubjectName)
                .HasMaxLength(500);

            this.Property(t => t.ExamGradeName)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ExamTypeWiseTabulationSheetDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ExamTypeId).HasColumnName("ExamTypeId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicClassesId).HasColumnName("AcademicClassesId");
            this.Property(t => t.AcademicClassName).HasColumnName("AcademicClassName");
            this.Property(t => t.SubjectAcademicClassMappingsMapId).HasColumnName("SubjectAcademicClassMappingsMapId");
            this.Property(t => t.SubjectMarks).HasColumnName("SubjectMarks");
            this.Property(t => t.AcademicGroupId).HasColumnName("AcademicGroupId");
            this.Property(t => t.AcademicGroupName).HasColumnName("AcademicGroupName");
            this.Property(t => t.AcademicClassesSectionMapId).HasColumnName("AcademicClassesSectionMapId");
            this.Property(t => t.AcademicSectionId).HasColumnName("AcademicSectionId");
            this.Property(t => t.AcademicSectionName).HasColumnName("AcademicSectionName");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.AcademicSessionName).HasColumnName("AcademicSessionName");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.InstituteSubjectClassId).HasColumnName("InstituteSubjectClassId");
            this.Property(t => t.InstituteSubjectName).HasColumnName("InstituteSubjectName");
            this.Property(t => t.TotalMarks).HasColumnName("TotalMarks");
            this.Property(t => t.PercentageTotalMarks).HasColumnName("PercentageTotalMarks");
            this.Property(t => t.AverageMarks).HasColumnName("AverageMarks");
            this.Property(t => t.AcceptTotalMarks).HasColumnName("AcceptTotalMarks");
            this.Property(t => t.ExamGradeId).HasColumnName("ExamGradeId");
            this.Property(t => t.ExamGradeName).HasColumnName("ExamGradeName");
            this.Property(t => t.ExamGradePoint).HasColumnName("ExamGradePoint");

            // Relationships
            this.HasRequired(t => t.ExamType)
                .WithMany(t => t.ExamTypeWiseTabulationSheetDetails)
                .HasForeignKey(d => d.ExamTypeId);

        }
    }
}
