using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FeesBoothMap : EntityTypeConfiguration<FeesBooth>
    {
        public FeesBoothMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Description)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("FeesBooth");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AcademicBracnchId).HasColumnName("AcademicBracnchId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.FeesBooths)
                .HasForeignKey(d => d.AcademicBracnchId);

        }
    }
}
