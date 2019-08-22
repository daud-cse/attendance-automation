using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class CertificatePrintMap : EntityTypeConfiguration<CertificatePrint>
    {
        public CertificatePrintMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Body)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("CertificatePrints");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CertificatePrintTypeId).HasColumnName("CertificatePrintTypeId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.CertificatePrintType)
                .WithMany(t => t.CertificatePrints)
                .HasForeignKey(d => d.CertificatePrintTypeId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.CertificatePrints)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
