using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FeesGenerateStudentMap : EntityTypeConfiguration<FeesGenerateStudent>
    {
        public FeesGenerateStudentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FeesGenerateStudents");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FeesGenerateId).HasColumnName("FeesGenerateId");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.TotalAmountDue).HasColumnName("TotalAmountDue");
            this.Property(t => t.AmountPaid).HasColumnName("AmountPaid");
            this.Property(t => t.AmountDue).HasColumnName("AmountDue");
            this.Property(t => t.IsCompleted).HasColumnName("IsCompleted");
            this.Property(t => t.IsPublished).HasColumnName("IsPublished");
            this.Property(t => t.HasAnyAdvance).HasColumnName("HasAnyAdvance");

            // Relationships
            this.HasRequired(t => t.FeesGenerate)
                .WithMany(t => t.FeesGenerateStudents)
                .HasForeignKey(d => d.FeesGenerateId);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.FeesGenerateStudents)
                .HasForeignKey(d => d.StudentId);

        }
    }
}
