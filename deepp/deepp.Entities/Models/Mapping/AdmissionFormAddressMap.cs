using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class AdmissionFormAddressMap : EntityTypeConfiguration<AdmissionFormAddress>
    {
        public AdmissionFormAddressMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.ZipCode)
                .HasMaxLength(32);

            this.Property(t => t.AddressBody)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("AdmissionFormAddresses");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.AdmissionFormId).HasColumnName("AdmissionFormId");
            this.Property(t => t.DistrictOrStateId).HasColumnName("DistrictOrStateId");
            this.Property(t => t.ZipCode).HasColumnName("ZipCode");
            this.Property(t => t.AddressBody).HasColumnName("AddressBody");

            // Relationships
            this.HasRequired(t => t.AdmissionForm)
                .WithMany(t => t.AdmissionFormAddresses)
                .HasForeignKey(d => d.AdmissionFormId);
            this.HasOptional(t => t.DistrictOrState)
                .WithMany(t => t.AdmissionFormAddresses)
                .HasForeignKey(d => d.DistrictOrStateId);

        }
    }
}
