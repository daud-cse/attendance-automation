using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class InstituteSubjectClassMap : EntityTypeConfiguration<InstituteSubjectClass>
    {
        public InstituteSubjectClassMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("InstituteSubjectClass");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteSubjectId).HasColumnName("InstituteSubjectId");
            this.Property(t => t.ClassId).HasColumnName("ClassId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.AcademicClass)
                .WithMany(t => t.InstituteSubjectClasses)
                .HasForeignKey(d => d.ClassId);
            this.HasRequired(t => t.InstituteSubject)
                .WithMany(t => t.InstituteSubjectClasses)
                .HasForeignKey(d => d.InstituteSubjectId);

        }
    }
}
