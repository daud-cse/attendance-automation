using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ScholarshipMap : EntityTypeConfiguration<Scholarship>
    {
        public ScholarshipMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Description)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("Scholarships");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.GlobalScholarshipId).HasColumnName("GlobalScholarshipId");

            // Relationships
            this.HasOptional(t => t.GlobalScholarship)
                .WithMany(t => t.Scholarships)
                .HasForeignKey(d => d.GlobalScholarshipId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Scholarships)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
