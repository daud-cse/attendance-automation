using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class BankAccountInfoMap : EntityTypeConfiguration<BankAccountInfo>
    {
        public BankAccountInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.BankAccountAutoId)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BankAccountNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.BankName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.BranchName)
                .HasMaxLength(150);

            this.Property(t => t.SetBy)
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("BankAccountInfo");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.BankAccountAutoId).HasColumnName("BankAccountAutoId");
            this.Property(t => t.BankAccountNo).HasColumnName("BankAccountNo");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.BankAccountTypeId).HasColumnName("BankAccountTypeId");
            this.Property(t => t.BankName).HasColumnName("BankName");
            this.Property(t => t.BranchName).HasColumnName("BranchName");
            this.Property(t => t.AccountOpeningDate).HasColumnName("AccountOpeningDate");
            this.Property(t => t.AccountClosingDate).HasColumnName("AccountClosingDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.OpeningBalance).HasColumnName("OpeningBalance");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");

            // Relationships
            this.HasRequired(t => t.BankAccountType)
                .WithMany(t => t.BankAccountInfoes)
                .HasForeignKey(d => d.BankAccountTypeId);

        }
    }
}
