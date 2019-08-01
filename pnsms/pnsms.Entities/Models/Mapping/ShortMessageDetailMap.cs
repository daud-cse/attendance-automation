using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class ShortMessageDetailMap : EntityTypeConfiguration<ShortMessageDetail>
    {
        public ShortMessageDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.SmsText)
                .HasMaxLength(512);

            this.Property(t => t.MobileNumber)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.GatewayIdentificationNo)
                .HasMaxLength(128);

            this.Property(t => t.Comments)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("ShortMessageDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ShortMessageId).HasColumnName("ShortMessageId");
            this.Property(t => t.SmsText).HasColumnName("SmsText");
            this.Property(t => t.MobileNumber).HasColumnName("MobileNumber");
            this.Property(t => t.UserInfoId).HasColumnName("UserInfoId");
            this.Property(t => t.SmsCount).HasColumnName("SmsCount");
            this.Property(t => t.IsSent).HasColumnName("IsSent");
            this.Property(t => t.ShortMessageStatusId).HasColumnName("ShortMessageStatusId");
            this.Property(t => t.GatewayIdentificationNo).HasColumnName("GatewayIdentificationNo");
            this.Property(t => t.IsStatusUpdated).HasColumnName("IsStatusUpdated");
            this.Property(t => t.StatusUpdatedAt).HasColumnName("StatusUpdatedAt");
            this.Property(t => t.Comments).HasColumnName("Comments");
            this.Property(t => t.StudentId).HasColumnName("StudentId");

            // Relationships
            this.HasRequired(t => t.ShortMessage)
                .WithMany(t => t.ShortMessageDetails)
                .HasForeignKey(d => d.ShortMessageId);
            this.HasOptional(t => t.ShortMessageStatus)
                .WithMany(t => t.ShortMessageDetails)
                .HasForeignKey(d => d.ShortMessageStatusId);
            this.HasOptional(t => t.Student)
                .WithMany(t => t.ShortMessageDetails)
                .HasForeignKey(d => d.StudentId);
            this.HasOptional(t => t.UserInfo)
                .WithMany(t => t.ShortMessageDetails)
                .HasForeignKey(d => d.UserInfoId);

        }
    }
}
