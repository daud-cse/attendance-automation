using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class DocumentMap : EntityTypeConfiguration<Document>
    {
        public DocumentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileExt)
                .HasMaxLength(50);

            this.Property(t => t.FileName)
                .HasMaxLength(1024);

            this.Property(t => t.DocumentType)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("Documents");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.DocumentTypeId).HasColumnName("DocumentTypeId");
            this.Property(t => t.RefPrimaryKey).HasColumnName("RefPrimaryKey");
            this.Property(t => t.Caption).HasColumnName("Caption");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.FileExt).HasColumnName("FileExt");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.FileName).HasColumnName("FileName");
            this.Property(t => t.DocumentType).HasColumnName("DocumentType");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdatedTime).HasColumnName("LastUpdatedTime");

            // Relationships
            this.HasRequired(t => t.DocumentType1)
                .WithMany(t => t.Documents)
                .HasForeignKey(d => d.DocumentTypeId);

        }
    }
}
