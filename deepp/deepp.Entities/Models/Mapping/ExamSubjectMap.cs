using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ExamSubjectMap : EntityTypeConfiguration<ExamSubject>
    {
        public ExamSubjectMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ExamTime)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ExamSubject");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ExamId).HasColumnName("ExamId");
            this.Property(t => t.TecherId).HasColumnName("TecherId");
            this.Property(t => t.InstituteSubjectClassId).HasColumnName("InstituteSubjectClassId");
            this.Property(t => t.TotalMarks).HasColumnName("TotalMarks");
            this.Property(t => t.PassMarks).HasColumnName("PassMarks");
            this.Property(t => t.HighestMarks).HasColumnName("HighestMarks");
            this.Property(t => t.ExamDate).HasColumnName("ExamDate");
            this.Property(t => t.ExamTime).HasColumnName("ExamTime");
            this.Property(t => t.TotalAttended).HasColumnName("TotalAttended");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Exam)
                .WithMany(t => t.ExamSubjects)
                .HasForeignKey(d => d.ExamId);
            this.HasRequired(t => t.InstituteSubjectClass)
                .WithMany(t => t.ExamSubjects)
                .HasForeignKey(d => d.InstituteSubjectClassId);
            this.HasRequired(t => t.Teacher)
                .WithMany(t => t.ExamSubjects)
                .HasForeignKey(d => d.TecherId);

        }
    }
}
