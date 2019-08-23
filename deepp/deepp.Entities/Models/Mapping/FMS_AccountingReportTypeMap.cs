using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FMS_AccountingReportTypeMap : EntityTypeConfiguration<FMS_AccountingReportType>
    {
        public FMS_AccountingReportTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ReportTypeID);

            // Properties
            this.Property(t => t.ReportTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TypeName)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("FMS_AccountingReportType");
            this.Property(t => t.ReportTypeID).HasColumnName("ReportTypeID");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
            this.Property(t => t.Description).HasColumnName("Description");
        }
    }
}
