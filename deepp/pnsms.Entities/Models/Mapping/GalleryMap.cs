using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class GalleryMap : EntityTypeConfiguration<Gallery>
    {
        public GalleryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.GalleryTitle)
                .HasMaxLength(512);

            this.Property(t => t.GallerySubtitle)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("Galleries");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.EventId).HasColumnName("EventId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.GalleryTitle).HasColumnName("GalleryTitle");
            this.Property(t => t.GallerySubtitle).HasColumnName("GallerySubtitle");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasOptional(t => t.Event)
                .WithMany(t => t.Galleries)
                .HasForeignKey(d => d.EventId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Galleries)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
