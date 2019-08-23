using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FMS_GLSubsidyTypeMapMap : EntityTypeConfiguration<FMS_GLSubsidyTypeMap>
    {
        public FMS_GLSubsidyTypeMapMap()
        {
            // Primary Key
            this.HasKey(t => t.GlSubsidyTypeMapId);

            // Properties
            this.Property(t => t.SetBy)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("FMS_GLSubsidyTypeMap");
            this.Property(t => t.GlSubsidyTypeMapId).HasColumnName("GlSubsidyTypeMapId");
            this.Property(t => t.GLAccountId).HasColumnName("GLAccountId");
            this.Property(t => t.SubsidyTypeId).HasColumnName("SubsidyTypeId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.SetBy).HasColumnName("SetBy");
            this.Property(t => t.SetDate).HasColumnName("SetDate");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");

            // Relationships
            this.HasRequired(t => t.FMS_GLAccount)
                .WithMany(t => t.FMS_GLSubsidyTypeMap)
                .HasForeignKey(d => d.GLAccountId);
            this.HasRequired(t => t.FMS_SubsidyType)
                .WithMany(t => t.FMS_GLSubsidyTypeMap)
                .HasForeignKey(d => d.SubsidyTypeId);

        }
    }
}
