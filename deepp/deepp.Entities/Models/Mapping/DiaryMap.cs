using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class DiaryMap : EntityTypeConfiguration<Diary>
    {
        public DiaryMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Subject)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("Diaries");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.DairyDate).HasColumnName("DairyDate");
            this.Property(t => t.RoutinePeriodId).HasColumnName("RoutinePeriodId");
            this.Property(t => t.SubjectId).HasColumnName("SubjectId");
            this.Property(t => t.TeacherId).HasColumnName("TeacherId");
            this.Property(t => t.BuildingRoomId).HasColumnName("BuildingRoomId");
            this.Property(t => t.Subject).HasColumnName("Subject");
            this.Property(t => t.Details).HasColumnName("Details");
            this.Property(t => t.Homework).HasColumnName("Homework");

            // Relationships
            this.HasOptional(t => t.BuildingRoom)
                .WithMany(t => t.Diaries)
                .HasForeignKey(d => d.BuildingRoomId);
            this.HasOptional(t => t.Employee)
                .WithMany(t => t.Diaries)
                .HasForeignKey(d => d.TeacherId);
            this.HasRequired(t => t.RoutinePeriod)
                .WithMany(t => t.Diaries)
                .HasForeignKey(d => d.RoutinePeriodId);
            this.HasRequired(t => t.Subject1)
                .WithMany(t => t.Diaries)
                .HasForeignKey(d => d.SubjectId);

        }
    }
}
