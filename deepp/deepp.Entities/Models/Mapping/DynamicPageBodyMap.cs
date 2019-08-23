using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class DynamicPageBodyMap : EntityTypeConfiguration<DynamicPageBody>
    {
        public DynamicPageBodyMap()
        {
            // Primary Key
            this.HasKey(t => t.DynamicPageId);

            // Properties
            this.Property(t => t.DynamicPageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("DynamicPageBodies");
            this.Property(t => t.DynamicPageId).HasColumnName("DynamicPageId");
            this.Property(t => t.HtmlText).HasColumnName("HtmlText");

            // Relationships
            this.HasRequired(t => t.DynamicPage)
                .WithOptional(t => t.DynamicPageBody);

        }
    }
}
