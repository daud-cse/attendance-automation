using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class ContentDetailMap : EntityTypeConfiguration<ContentDetail>
    {
        public ContentDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("ContentDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ContentId).HasColumnName("ContentId");
            this.Property(t => t.TagId).HasColumnName("TagId");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.ContentDetails)
                .HasForeignKey(d => d.ContentId);
            this.HasRequired(t => t.Tag)
                .WithMany(t => t.ContentDetails)
                .HasForeignKey(d => d.TagId);

        }
    }
}
