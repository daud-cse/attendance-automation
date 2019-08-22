using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class InstituteSubjectMap : EntityTypeConfiguration<InstituteSubject>
    {
        public InstituteSubjectMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("InstituteSubject");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.SubjectId).HasColumnName("SubjectId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.InstituteSubjects)
                .HasForeignKey(d => d.InstituteId);
            this.HasRequired(t => t.Subject)
                .WithMany(t => t.InstituteSubjects)
                .HasForeignKey(d => d.SubjectId);

        }
    }
}
