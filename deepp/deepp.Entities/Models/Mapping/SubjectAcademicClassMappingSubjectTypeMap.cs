using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class SubjectAcademicClassMappingSubjectTypeMap : EntityTypeConfiguration<SubjectAcademicClassMappingSubjectType>
    {
        public SubjectAcademicClassMappingSubjectTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("SubjectAcademicClassMappingSubjectTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SubjectAcademicClassMappingId).HasColumnName("SubjectAcademicClassMappingId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicClassId).HasColumnName("AcademicClassId");
            this.Property(t => t.SubjectId).HasColumnName("SubjectId");
            this.Property(t => t.SubjectTypeId).HasColumnName("SubjectTypeId");

            // Relationships
            this.HasRequired(t => t.AcademicClass)
                .WithMany(t => t.SubjectAcademicClassMappingSubjectTypes)
                .HasForeignKey(d => d.AcademicClassId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.SubjectAcademicClassMappingSubjectTypes)
                .HasForeignKey(d => d.InstituteId);
            this.HasRequired(t => t.Subject)
                .WithMany(t => t.SubjectAcademicClassMappingSubjectTypes)
                .HasForeignKey(d => d.SubjectId);
            this.HasRequired(t => t.SubjectAcademicClassMapping)
                .WithMany(t => t.SubjectAcademicClassMappingSubjectTypes)
                .HasForeignKey(d => d.SubjectAcademicClassMappingId);
            this.HasRequired(t => t.SubjectType)
                .WithMany(t => t.SubjectAcademicClassMappingSubjectTypes)
                .HasForeignKey(d => d.SubjectTypeId);

        }
    }
}
