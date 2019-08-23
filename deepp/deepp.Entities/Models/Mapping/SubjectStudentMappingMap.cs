using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class SubjectStudentMappingMap : EntityTypeConfiguration<SubjectStudentMapping>
    {
        public SubjectStudentMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SubjectStudentMappings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicSessionId).HasColumnName("AcademicSessionId");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.SubjectId).HasColumnName("SubjectId");
            this.Property(t => t.SubjectTypeId).HasColumnName("SubjectTypeId");

            // Relationships
            this.HasRequired(t => t.AcademicSession)
                .WithMany(t => t.SubjectStudentMappings)
                .HasForeignKey(d => d.AcademicSessionId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.SubjectStudentMappings)
                .HasForeignKey(d => d.InstituteId);
            this.HasRequired(t => t.Student)
                .WithMany(t => t.SubjectStudentMappings)
                .HasForeignKey(d => d.StudentId);
            this.HasRequired(t => t.Subject)
                .WithMany(t => t.SubjectStudentMappings)
                .HasForeignKey(d => d.SubjectId);
            this.HasOptional(t => t.SubjectType)
                .WithMany(t => t.SubjectStudentMappings)
                .HasForeignKey(d => d.SubjectTypeId);

        }
    }
}
