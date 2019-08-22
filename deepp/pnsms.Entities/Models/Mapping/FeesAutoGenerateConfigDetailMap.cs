using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class FeesAutoGenerateConfigDetailMap : EntityTypeConfiguration<FeesAutoGenerateConfigDetail>
    {
        public FeesAutoGenerateConfigDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FeesAutoGenerateConfigDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FeesAutoGenerateConfigId).HasColumnName("FeesAutoGenerateConfigId");
            this.Property(t => t.FeesHeadId).HasColumnName("FeesHeadId");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.FeesAutoGenerateConfig)
                .WithMany(t => t.FeesAutoGenerateConfigDetails)
                .HasForeignKey(d => d.FeesAutoGenerateConfigId);
            this.HasRequired(t => t.FeesHead)
                .WithMany(t => t.FeesAutoGenerateConfigDetails)
                .HasForeignKey(d => d.FeesHeadId);

        }
    }
}
