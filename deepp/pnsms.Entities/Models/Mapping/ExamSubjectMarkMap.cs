using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class ExamSubjectMarkMap : EntityTypeConfiguration<ExamSubjectMark>
    {
        public ExamSubjectMarkMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Comment)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ExamSubjectMarks");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.ExamId).HasColumnName("ExamId");
            this.Property(t => t.SubjectAcademicClassMappingsMapId).HasColumnName("SubjectAcademicClassMappingsMapId");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.MarksObtained).HasColumnName("MarksObtained");
            this.Property(t => t.AcceptMarksTotal).HasColumnName("AcceptMarksTotal");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.Exam)
                .WithMany(t => t.ExamSubjectMarks)
                .HasForeignKey(d => d.ExamId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.ExamSubjectMarks)
                .HasForeignKey(d => d.InstituteId);
            this.HasRequired(t => t.SubjectAcademicClassMapping)
                .WithMany(t => t.ExamSubjectMarks)
                .HasForeignKey(d => d.SubjectAcademicClassMappingsMapId);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.ExamSubjectMarks)
                .HasForeignKey(d => d.StudentId);

        }
    }
}
