using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FMS_GLAccountTemplateMap : EntityTypeConfiguration<FMS_GLAccountTemplate>
    {
        public FMS_GLAccountTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.GLAccountId);

            // Properties
            this.Property(t => t.GLAccountId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.GLAccountCode)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.GLAccountName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.GLAccountTreeName)
                .HasMaxLength(50);

            this.Property(t => t.SubCtrlPrefix)
                .HasMaxLength(100);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.DrCr)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("FMS_GLAccountTemplate");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.GLAccountCode).HasColumnName("GLAccountCode");
            this.Property(t => t.GLAccountName).HasColumnName("GLAccountName");
            this.Property(t => t.GLAccountTreeName).HasColumnName("GLAccountTreeName");
            this.Property(t => t.ParentAccountId).HasColumnName("ParentAccountId");
            this.Property(t => t.SubCtrlPrefix).HasColumnName("SubCtrlPrefix");
            this.Property(t => t.AccountTypeID).HasColumnName("AccountTypeID");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.OpeningDate).HasColumnName("OpeningDate");
            this.Property(t => t.LevelCode).HasColumnName("LevelCode");
            this.Property(t => t.SortOrder).HasColumnName("SortOrder");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.CurrentBalance).HasColumnName("CurrentBalance");
            this.Property(t => t.CurrentYearOpeningBalance).HasColumnName("CurrentYearOpeningBalance");
            this.Property(t => t.IsSubsidyExist).HasColumnName("IsSubsidyExist");
            this.Property(t => t.HasChild).HasColumnName("HasChild");
            this.Property(t => t.ReportTypeID).HasColumnName("ReportTypeID");
            this.Property(t => t.DrCr).HasColumnName("DrCr");
        }
    }
}
