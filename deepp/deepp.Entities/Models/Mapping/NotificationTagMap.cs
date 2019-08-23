using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class NotificationTagMap : EntityTypeConfiguration<NotificationTag>
    {
        public NotificationTagMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Tag)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.PreviewText)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.TextToCalculate)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("NotificationTags");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NotificationGroupId).HasColumnName("NotificationGroupId");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.MaxCharLength).HasColumnName("MaxCharLength");
            this.Property(t => t.IsForStudent).HasColumnName("IsForStudent");
            this.Property(t => t.IsForGuardian).HasColumnName("IsForGuardian");
            this.Property(t => t.IsForTeacher).HasColumnName("IsForTeacher");
            this.Property(t => t.IsForEmployee).HasColumnName("IsForEmployee");
            this.Property(t => t.IsForGoverningBody).HasColumnName("IsForGoverningBody");
            this.Property(t => t.PreviewText).HasColumnName("PreviewText");
            this.Property(t => t.TextToCalculate).HasColumnName("TextToCalculate");
            this.Property(t => t.IsShowFromDate).HasColumnName("IsShowFromDate");
            this.Property(t => t.IsShowToDate).HasColumnName("IsShowToDate");

            // Relationships
            this.HasRequired(t => t.NotificationTagGroup)
                .WithMany(t => t.NotificationTags)
                .HasForeignKey(d => d.NotificationGroupId);

        }
    }
}
