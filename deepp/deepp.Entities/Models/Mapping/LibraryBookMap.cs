using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class LibraryBookMap : EntityTypeConfiguration<LibraryBook>
    {
        public LibraryBookMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(1024);

            this.Property(t => t.ISBN)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("LibraryBooks");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.ISBN).HasColumnName("ISBN");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.LibraryBooks)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
