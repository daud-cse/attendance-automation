using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class BankAccountTypeMap : EntityTypeConfiguration<BankAccountType>
    {
        public BankAccountTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.BankAccountTypeId);

            // Properties
            this.Property(t => t.BankAccountTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.BankAccountTypeName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("BankAccountType");
            this.Property(t => t.BankAccountTypeId).HasColumnName("BankAccountTypeId");
            this.Property(t => t.BankAccountTypeName).HasColumnName("BankAccountTypeName");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
        }
    }
}
