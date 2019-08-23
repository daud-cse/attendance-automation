using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class NoticeMap : EntityTypeConfiguration<Notice>
    {
        public NoticeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NoticeTitle)
                .HasMaxLength(512);

            // Table & Column Mappings
            this.ToTable("Notices");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.StartDate).HasColumnName("StartDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.NoticeTitle).HasColumnName("NoticeTitle");
            this.Property(t => t.NoticeBody).HasColumnName("NoticeBody");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.NoticeTypeId).HasColumnName("NoticeTypeId");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.Notices)
                .HasForeignKey(d => d.InstituteId);
            this.HasOptional(t => t.NoticeType)
                .WithMany(t => t.Notices)
                .HasForeignKey(d => d.NoticeTypeId);

        }
    }
}
