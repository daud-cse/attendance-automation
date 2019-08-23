using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class DistrictOrStateMap : EntityTypeConfiguration<DistrictOrState>
    {
        public DistrictOrStateMap()
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
            this.ToTable("DistrictOrStates");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CountryId).HasColumnName("CountryId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.GlobalDistrictId).HasColumnName("GlobalDistrictId");

            // Relationships
            this.HasRequired(t => t.Country)
                .WithMany(t => t.DistrictOrStates)
                .HasForeignKey(d => d.CountryId);
            this.HasOptional(t => t.GlobalDistrict)
                .WithMany(t => t.DistrictOrStates)
                .HasForeignKey(d => d.GlobalDistrictId);

        }
    }
}
