using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class GlobalDistrictMap : EntityTypeConfiguration<GlobalDistrict>
    {
        public GlobalDistrictMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("GlobalDistricts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GlobalDivisionId).HasColumnName("GlobalDivisionId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.GlobalDivision)
                .WithMany(t => t.GlobalDistricts)
                .HasForeignKey(d => d.GlobalDivisionId);

        }
    }
}
