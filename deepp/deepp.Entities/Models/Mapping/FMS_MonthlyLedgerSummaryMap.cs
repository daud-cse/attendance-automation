using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FMS_MonthlyLedgerSummaryMap : EntityTypeConfiguration<FMS_MonthlyLedgerSummary>
    {
        public FMS_MonthlyLedgerSummaryMap()
        {
            // Primary Key
            this.HasKey(t => new { t.GLAccountId, t.SummaryYM });

            // Properties
            this.Property(t => t.GLAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SummaryYM)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("FMS_MonthlyLedgerSummary");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.SummaryYM).HasColumnName("SummaryYM");
            this.Property(t => t.Year).HasColumnName("Year");
            this.Property(t => t.Month).HasColumnName("Month");
            this.Property(t => t.DrAmount).HasColumnName("DrAmount");
            this.Property(t => t.CrAmount).HasColumnName("CrAmount");
            this.Property(t => t.Balance).HasColumnName("Balance");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
        }
    }
}
