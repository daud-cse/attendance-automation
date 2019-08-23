using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FeesCollectionDetailMap : EntityTypeConfiguration<FeesCollectionDetail>
    {
        public FeesCollectionDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("FeesCollectionDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.FeesCollectionId).HasColumnName("FeesCollectionId");
            this.Property(t => t.FeesGenerateId).HasColumnName("FeesGenerateId");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.IsCompleted).HasColumnName("IsCompleted");

            // Relationships
            this.HasRequired(t => t.FeesCollection)
                .WithMany(t => t.FeesCollectionDetails)
                .HasForeignKey(d => d.FeesCollectionId);
            this.HasRequired(t => t.FeesGenerate)
                .WithMany(t => t.FeesCollectionDetails)
                .HasForeignKey(d => d.FeesGenerateId);

        }
    }
}
