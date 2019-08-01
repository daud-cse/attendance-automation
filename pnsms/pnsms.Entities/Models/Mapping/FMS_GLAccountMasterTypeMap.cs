using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FMS_GLAccountMasterTypeMap : EntityTypeConfiguration<FMS_GLAccountMasterType>
    {
        public FMS_GLAccountMasterTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.MasterTypeID);

            // Properties
            this.Property(t => t.MasterTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.MasterTypeName)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            this.Property(t => t.DrCr)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("FMS_GLAccountMasterType");
            this.Property(t => t.MasterTypeID).HasColumnName("MasterTypeID");
            this.Property(t => t.MasterTypeName).HasColumnName("MasterTypeName");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.DrCr).HasColumnName("DrCr");
        }
    }
}
