using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FMS_RelatedAccountMap : EntityTypeConfiguration<FMS_RelatedAccount>
    {
        public FMS_RelatedAccountMap()
        {
            // Primary Key
            this.HasKey(t => t.RelatedAccountId);

            // Properties
            // Table & Column Mappings
            this.ToTable("FMS_RelatedAccount");
            this.Property(t => t.RelatedAccountId).HasColumnName("RelatedAccountId");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.TransactionTypeId).HasColumnName("TransactionTypeId");
            this.Property(t => t.DebitAccountRestrictionTypeId).HasColumnName("DebitAccountRestrictionTypeId");
            this.Property(t => t.CreditAccountRestrictionTypeId).HasColumnName("CreditAccountRestrictionTypeId");
            this.Property(t => t.SourceAccountId).HasColumnName("SourceAccountId");
            this.Property(t => t.CompromisedAccountId).HasColumnName("CompromisedAccountId");
            this.Property(t => t.LedgerTypeId).HasColumnName("LedgerTypeId");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
