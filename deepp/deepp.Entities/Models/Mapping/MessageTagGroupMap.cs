using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class MessageTagGroupMap : EntityTypeConfiguration<MessageTagGroup>
    {
        public MessageTagGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("MessageTagGroups");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IsForStudent).HasColumnName("IsForStudent");
            this.Property(t => t.IsForTeacher).HasColumnName("IsForTeacher");
            this.Property(t => t.IsForEmployee).HasColumnName("IsForEmployee");
        }
    }
}
