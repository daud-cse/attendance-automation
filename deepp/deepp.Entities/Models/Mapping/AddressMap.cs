using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ZipCode)
                .HasMaxLength(32);

            this.Property(t => t.AddressBody)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("Addresses");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AddressTypeId).HasColumnName("AddressTypeId");
            this.Property(t => t.RefPrimaryKey).HasColumnName("RefPrimaryKey");
            this.Property(t => t.DistrictOrStateId).HasColumnName("DistrictOrStateId");
            this.Property(t => t.ZipCode).HasColumnName("ZipCode");
            this.Property(t => t.AddressBody).HasColumnName("AddressBody");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.AddressType)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.AddressTypeId);
            this.HasOptional(t => t.DistrictOrState)
                .WithMany(t => t.Addresses)
                .HasForeignKey(d => d.DistrictOrStateId);

        }
    }
}
