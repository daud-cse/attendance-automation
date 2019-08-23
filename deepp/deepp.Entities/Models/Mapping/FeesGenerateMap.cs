using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class FeesGenerateMap : EntityTypeConfiguration<FeesGenerate>
    {
        public FeesGenerateMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Remarks)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("FeesGenerates");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.ForTheMonth).HasColumnName("ForTheMonth");
            this.Property(t => t.ForTheYear).HasColumnName("ForTheYear");
            this.Property(t => t.ForTheDate).HasColumnName("ForTheDate");
            this.Property(t => t.GenerationDate).HasColumnName("GenerationDate");
            this.Property(t => t.DueDate).HasColumnName("DueDate");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.IsPublished).HasColumnName("IsPublished");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.FeesGenerates)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
