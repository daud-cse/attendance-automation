using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class EducationalQualificationMap : EntityTypeConfiguration<EducationalQualification>
    {
        public EducationalQualificationMap()
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
            this.ToTable("EducationalQualifications");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.GlobalEducationalQualificationId).HasColumnName("GlobalEducationalQualificationId");

            // Relationships
            this.HasOptional(t => t.GlobalEducationalQualification)
                .WithMany(t => t.EducationalQualifications)
                .HasForeignKey(d => d.GlobalEducationalQualificationId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.EducationalQualifications)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
