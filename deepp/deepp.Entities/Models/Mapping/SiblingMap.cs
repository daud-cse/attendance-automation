using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class SiblingMap : EntityTypeConfiguration<Sibling>
    {
        public SiblingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("Siblings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StudentId).HasColumnName("StudentId");
            this.Property(t => t.SiblingId).HasColumnName("SiblingId");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");

            // Relationships
            this.HasRequired(t => t.Student)
                .WithMany(t => t.Siblings)
                .HasForeignKey(d => d.SiblingId);
            this.HasRequired(t => t.Student1)
                .WithMany(t => t.Siblings1)
                .HasForeignKey(d => d.StudentId);

        }
    }
}
