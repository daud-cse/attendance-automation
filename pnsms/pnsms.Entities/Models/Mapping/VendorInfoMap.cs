using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class VendorInfoMap : EntityTypeConfiguration<VendorInfo>
    {
        public VendorInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.VendorAutoId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.VendorAddress)
                .HasMaxLength(500);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("VendorInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.VendorAutoId).HasColumnName("VendorAutoId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.VendorAddress).HasColumnName("VendorAddress");
            this.Property(t => t.VendorTypeId).HasColumnName("VendorTypeId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.OpeningBalance).HasColumnName("OpeningBalance");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");

            // Relationships
            this.HasRequired(t => t.VendorType)
                .WithMany(t => t.VendorInfoes)
                .HasForeignKey(d => d.VendorTypeId);

        }
    }
}
