using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ImageMap : EntityTypeConfiguration<Image>
    {
        public ImageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileExt)
                .HasMaxLength(50);

            this.Property(t => t.FileName)
                .HasMaxLength(1024);

            this.Property(t => t.ContentType)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("Images");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.RefTypeId).HasColumnName("RefTypeId");
            this.Property(t => t.RefPrimaryKey).HasColumnName("RefPrimaryKey");
            this.Property(t => t.ImageBinaryData).HasColumnName("ImageBinaryData");
            this.Property(t => t.ImageCaption).HasColumnName("ImageCaption");
            this.Property(t => t.ImageUrl).HasColumnName("ImageUrl");
            this.Property(t => t.FileExt).HasColumnName("FileExt");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdatedTime).HasColumnName("LastUpdatedTime");
            this.Property(t => t.ImageDescription).HasColumnName("ImageDescription");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.ContentType).HasColumnName("ContentType");

            // Relationships
            this.HasRequired(t => t.ImageType)
                .WithMany(t => t.Images)
                .HasForeignKey(d => d.RefTypeId);

        }
    }
}
