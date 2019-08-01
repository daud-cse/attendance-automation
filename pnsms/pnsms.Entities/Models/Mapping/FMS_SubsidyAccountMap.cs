using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FMS_SubsidyAccountMap : EntityTypeConfiguration<FMS_SubsidyAccount>
    {
        public FMS_SubsidyAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.SubsidyAccountId);

            // Properties
            this.Property(t => t.SubsidyName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BankName)
                .HasMaxLength(200);

            this.Property(t => t.BankAccountNo)
                .HasMaxLength(100);

            this.Property(t => t.BranchName)
                .HasMaxLength(200);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ActionType)
                .HasMaxLength(6);

            // Table & Column Mappings
            this.ToTable("FMS_SubsidyAccount");
            this.Property(t => t.SubsidyAccountId).HasColumnName("SubsidyAccountId");
            this.Property(t => t.SubsidyTypeID).HasColumnName("SubsidyTypeID");
            this.Property(t => t.SubsidyId).HasColumnName("SubsidyId");
            this.Property(t => t.SubsidyName).HasColumnName("SubsidyName");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BankAccountNo).HasColumnName("BankAccountNo");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.ActionType).HasColumnName("ActionType");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
        }
    }
}
