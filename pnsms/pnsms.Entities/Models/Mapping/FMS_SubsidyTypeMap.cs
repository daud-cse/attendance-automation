using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FMS_SubsidyTypeMap : EntityTypeConfiguration<FMS_SubsidyType>
    {
        public FMS_SubsidyTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.SubsidyTypeId);

            // Properties
            this.Property(t => t.SubsidyTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SubsidyTypeName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TableName)
                .HasMaxLength(100);

            this.Property(t => t.DRCR)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("FMS_SubsidyType");
            this.Property(t => t.SubsidyTypeId).HasColumnName("SubsidyTypeId");
            this.Property(t => t.SubsidyTypeName).HasColumnName("SubsidyTypeName");
            this.Property(t => t.TableName).HasColumnName("TableName");
            this.Property(t => t.DRCR).HasColumnName("DRCR");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
        }
    }
}
