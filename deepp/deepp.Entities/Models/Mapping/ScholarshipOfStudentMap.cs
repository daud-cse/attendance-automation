using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ScholarshipOfStudentMap : EntityTypeConfiguration<ScholarshipOfStudent>
    {
        public ScholarshipOfStudentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ScholarshipOfStudents");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.ScholarshipId).HasColumnName("ScholarshipId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");

            // Relationships
            this.HasRequired(t => t.AcademicSession)
                .WithMany(t => t.ScholarshipOfStudents)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasRequired(t => t.Scholarship)
                .WithMany(t => t.ScholarshipOfStudents)
                .HasForeignKey(d => d.ScholarshipId);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.ScholarshipOfStudents)
                .HasForeignKey(d => d.StudentId);

        }
    }
}
