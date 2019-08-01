using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class SubjectMap : EntityTypeConfiguration<Subject>
    {
        public SubjectMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .HasMaxLength(50);

            this.Property(t => t.Code)
                .HasMaxLength(5);

            this.Property(t => t.ShortName)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("Subject");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ParentSubjectId).HasColumnName("ParentSubjectId");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.TagId).HasColumnName("TagId");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.OrderBy).HasColumnName("OrderBy");
            this.Property(t => t.ShortName).HasColumnName("ShortName");

            // Relationships
            this.HasOptional(t => t.Subject2)
                .WithMany(t => t.Subject1)
                .HasForeignKey(d => d.ParentSubjectId);
            this.HasOptional(t => t.Tag)
                .WithMany(t => t.Subjects)
                .HasForeignKey(d => d.TagId);

        }
    }
}
