using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class GlobalSubDistrictMap : EntityTypeConfiguration<GlobalSubDistrict>
    {
        public GlobalSubDistrictMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("GlobalSubDistricts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.GlobalDistrictId).HasColumnName("GlobalDistrictId");
            this.Property(t => t.GlobalSubDistrictTypeId).HasColumnName("GlobalSubDistrictTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.GlobalDistrict)
                .WithMany(t => t.GlobalSubDistricts)
                .HasForeignKey(d => d.GlobalDistrictId);
            this.HasRequired(t => t.GlobalSubDistrictType)
                .WithMany(t => t.GlobalSubDistricts)
                .HasForeignKey(d => d.GlobalSubDistrictTypeId);

        }
    }
}
