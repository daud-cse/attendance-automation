using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class ShortMessageMap : EntityTypeConfiguration<ShortMessage>
    {
        public ShortMessageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SmsTemplate)
                .IsRequired()
                .HasMaxLength(512);

            this.Property(t => t.Mask)
                .HasMaxLength(16);

            this.Property(t => t.SmsPreview)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("ShortMessages");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.SmsTemplate).HasColumnName("SmsTemplate");
            this.Property(t => t.IsStaticSms).HasColumnName("IsStaticSms");
            this.Property(t => t.SendAt).HasColumnName("SendAt");
            this.Property(t => t.TotalSmsCount).HasColumnName("TotalSmsCount");
            this.Property(t => t.Mask).HasColumnName("Mask");
            this.Property(t => t.IsSent).HasColumnName("IsSent");
            this.Property(t => t.IsGenerated).HasColumnName("IsGenerated");
            this.Property(t => t.IsChecked).HasColumnName("IsChecked");
            this.Property(t => t.DateFrom).HasColumnName("DateFrom");
            this.Property(t => t.DateTo).HasColumnName("DateTo");
            this.Property(t => t.IsFromWebPanel).HasColumnName("IsFromWebPanel");
            this.Property(t => t.SmsPreview).HasColumnName("SmsPreview");
            this.Property(t => t.IsPayByReceipient).HasColumnName("IsPayByReceipient");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.ShortMessages)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
