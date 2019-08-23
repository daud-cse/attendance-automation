using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ExamMap : EntityTypeConfiguration<Exam>
    {
        public ExamMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(125);

            this.Property(t => t.ExamTime)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Exam");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicClassesId).HasColumnName("AcademicClassesId");
            this.Property(t => t.AcademicClassesSectionMapId).HasColumnName("AcademicClassesSectionMapId");
            this.Property(t => t.ExamTypeId).HasColumnName("ExamTypeId");
            this.Property(t => t.IsGroupExam).HasColumnName("IsGroupExam");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ExamDateFrom).HasColumnName("ExamDateFrom");
            this.Property(t => t.ExamDateTo).HasColumnName("ExamDateTo");
            this.Property(t => t.ExamTime).HasColumnName("ExamTime");
            this.Property(t => t.TotalMarks).HasColumnName("TotalMarks");
            this.Property(t => t.HighestMarks).HasColumnName("HighestMarks");
            this.Property(t => t.PassMarks).HasColumnName("PassMarks");
            this.Property(t => t.AcceptMarks).HasColumnName("AcceptMarks");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.Exams)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.AcademicClass)
                .WithMany(t => t.Exams)
                .HasForeignKey(d => d.AcademicClassesId);
            this.HasOptional(t => t.AcademicClassSectionMapping)
                .WithMany(t => t.Exams)
                .HasForeignKey(d => d.AcademicClassesSectionMapId);
            this.HasRequired(t => t.ExamType)
                .WithMany(t => t.Exams)
                .HasForeignKey(d => d.ExamTypeId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Exams)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
