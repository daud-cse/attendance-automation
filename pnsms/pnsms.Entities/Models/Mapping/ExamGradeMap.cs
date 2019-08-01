using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class ExamGradeMap : EntityTypeConfiguration<ExamGrade>
    {
        public ExamGradeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.GradeName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Comment)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("ExamGrades");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.GradeName).HasColumnName("GradeName");
            this.Property(t => t.GradePoint).HasColumnName("GradePoint");
            this.Property(t => t.MarksFrom).HasColumnName("MarksFrom");
            this.Property(t => t.MarksUpto).HasColumnName("MarksUpto");
            this.Property(t => t.Comment).HasColumnName("Comment");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateDate).HasColumnName("LastUpdateDate");
        }
    }
}
