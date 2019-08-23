using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class MessageTagMap : EntityTypeConfiguration<MessageTag>
    {
        public MessageTagMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TagName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.TagDescription)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("MessageTags");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MessageTagGroupId).HasColumnName("MessageTagGroupId");
            this.Property(t => t.TagName).HasColumnName("TagName");
            this.Property(t => t.TagDescription).HasColumnName("TagDescription");
            this.Property(t => t.CharCount).HasColumnName("CharCount");

            // Relationships
            this.HasRequired(t => t.MessageTagGroup)
                .WithMany(t => t.MessageTags)
                .HasForeignKey(d => d.MessageTagGroupId);

        }
    }
}
