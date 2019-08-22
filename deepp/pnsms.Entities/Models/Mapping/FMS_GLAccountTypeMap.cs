using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FMS_GLAccountTypeMap : EntityTypeConfiguration<FMS_GLAccountType>
    {
        public FMS_GLAccountTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.TypeID);

            // Properties
            this.Property(t => t.TypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TypeName)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            this.Property(t => t.DrCr)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("FMS_GLAccountType");
            this.Property(t => t.TypeID).HasColumnName("TypeID");
            this.Property(t => t.MasterTypeID).HasColumnName("MasterTypeID");
            this.Property(t => t.TypeName).HasColumnName("TypeName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.AppearInReportTypeID).HasColumnName("AppearInReportTypeID");
            this.Property(t => t.DrCr).HasColumnName("DrCr");
        }
    }
}
