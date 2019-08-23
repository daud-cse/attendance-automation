using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class VoucherMap : EntityTypeConfiguration<Voucher>
    {
        public VoucherMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Vouchers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.VoucherDate).HasColumnName("VoucherDate");
            this.Property(t => t.Narration).HasColumnName("Narration");
            this.Property(t => t.TotalAmount).HasColumnName("TotalAmount");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.IsIncome).HasColumnName("IsIncome");
            this.Property(t => t.IsExpense).HasColumnName("IsExpense");

            // Relationships
            this.HasRequired(t => t.AcademicBranch)
                .WithMany(t => t.Vouchers)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Vouchers)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
