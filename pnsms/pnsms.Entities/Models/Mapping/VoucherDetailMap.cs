using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class VoucherDetailMap : EntityTypeConfiguration<VoucherDetail>
    {
        public VoucherDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("VoucherDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.VoucherId).HasColumnName("VoucherId");
            this.Property(t => t.ChartOfAccountId).HasColumnName("ChartOfAccountId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.Narration).HasColumnName("Narration");

            // Relationships
            this.HasRequired(t => t.ChartOfAccount)
                .WithMany(t => t.VoucherDetails)
                .HasForeignKey(d => d.ChartOfAccountId);
            this.HasRequired(t => t.Voucher)
                .WithMany(t => t.VoucherDetails)
                .HasForeignKey(d => d.VoucherId);

        }
    }
}
