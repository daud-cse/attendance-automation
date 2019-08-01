using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class VendorTypeMap : EntityTypeConfiguration<VendorType>
    {
        public VendorTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.VendorTypeId);

            // Properties
            this.Property(t => t.VendorTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.VendorTypeName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("VendorType");
            this.Property(t => t.VendorTypeId).HasColumnName("VendorTypeId");
            this.Property(t => t.VendorTypeName).HasColumnName("VendorTypeName");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
        }
    }
}
