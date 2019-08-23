using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ShortMessageTemplateMap : EntityTypeConfiguration<ShortMessageTemplate>
    {
        public ShortMessageTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SmsTemplate)
                .IsRequired()
                .HasMaxLength(512);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("ShortMessageTemplates");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.SmsTemplate).HasColumnName("SmsTemplate");
            this.Property(t => t.IsStaticSms).HasColumnName("IsStaticSms");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.IsForGeneral).HasColumnName("IsForGeneral");
            this.Property(t => t.IsForStudent).HasColumnName("IsForStudent");
            this.Property(t => t.IsForGuardian).HasColumnName("IsForGuardian");
            this.Property(t => t.IsForTeacher).HasColumnName("IsForTeacher");
            this.Property(t => t.IsForEmployee).HasColumnName("IsForEmployee");
            this.Property(t => t.IsForGoverningBody).HasColumnName("IsForGoverningBody");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.SmsCount).HasColumnName("SmsCount");
            this.Property(t => t.Name).HasColumnName("Name");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.ShortMessageTemplates)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
