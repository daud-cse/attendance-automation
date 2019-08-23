using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FeesGenerateHeadMap : EntityTypeConfiguration<FeesGenerateHead>
    {
        public FeesGenerateHeadMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FeesGenerateHeads");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FeesGenerateId).HasColumnName("FeesGenerateId");
            this.Property(t => t.FeesHeadId).HasColumnName("FeesHeadId");
            this.Property(t => t.Amount).HasColumnName("Amount");

            // Relationships
            this.HasRequired(t => t.FeesGenerate)
                .WithMany(t => t.FeesGenerateHeads)
                .HasForeignKey(d => d.FeesGenerateId);
            this.HasRequired(t => t.FeesHead)
                .WithMany(t => t.FeesGenerateHeads)
                .HasForeignKey(d => d.FeesHeadId);

        }
    }
}
