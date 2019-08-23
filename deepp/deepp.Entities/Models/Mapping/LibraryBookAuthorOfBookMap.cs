using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class LibraryBookAuthorOfBookMap : EntityTypeConfiguration<LibraryBookAuthorOfBook>
    {
        public LibraryBookAuthorOfBookMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("LibraryBookAuthorOfBooks");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.LibraryBookId).HasColumnName("LibraryBookId");
            this.Property(t => t.LibraryBookAuthorId).HasColumnName("LibraryBookAuthorId");

            // Relationships
            this.HasRequired(t => t.LibraryBookAuthore)
                .WithMany(t => t.LibraryBookAuthorOfBooks)
                .HasForeignKey(d => d.LibraryBookAuthorId);
            this.HasRequired(t => t.LibraryBook)
                .WithMany(t => t.LibraryBookAuthorOfBooks)
                .HasForeignKey(d => d.LibraryBookId);

        }
    }
}
