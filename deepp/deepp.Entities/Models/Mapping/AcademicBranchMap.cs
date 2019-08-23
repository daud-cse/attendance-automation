using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class AcademicBranchMap : EntityTypeConfiguration<AcademicBranch>
    {
        public AcademicBranchMap()
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
            this.ToTable("AcademicBranches");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.About).HasColumnName("About");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Latitude).HasColumnName("Latitude");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.WelComeText).HasColumnName("WelComeText");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.TagId).HasColumnName("TagId");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.AcademicBranches)
                .HasForeignKey(d => d.InstituteId);
            this.HasOptional(t => t.Tag)
                .WithMany(t => t.AcademicBranches)
                .HasForeignKey(d => d.TagId);

        }
    }
}
