using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class AttendanceTypeMap : EntityTypeConfiguration<AttendanceType>
    {
        public AttendanceTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Flag)
                .IsRequired()
                .HasMaxLength(2);

            this.Property(t => t.Description)
                .HasMaxLength(1024);

            // Table & Column Mappings
            this.ToTable("AttendanceTypes");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Flag).HasColumnName("Flag");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.IsUsedForStudent).HasColumnName("IsUsedForStudent");
            this.Property(t => t.IsUsedForEmployee).HasColumnName("IsUsedForEmployee");
            this.Property(t => t.IsUsedForTeacher).HasColumnName("IsUsedForTeacher");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.ColourId).HasColumnName("ColourId");
            this.Property(t => t.IsDefault).HasColumnName("IsDefault");
            this.Property(t => t.IsPresent).HasColumnName("IsPresent");
            this.Property(t => t.ShowAtAttendanceEntry).HasColumnName("ShowAtAttendanceEntry");
            this.Property(t => t.ShowAtLeaveEntry).HasColumnName("ShowAtLeaveEntry");

            // Relationships
            this.HasRequired(t => t.Colour)
                .WithMany(t => t.AttendanceTypes)
                .HasForeignKey(d => d.ColourId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.AttendanceTypes)
                .HasForeignKey(d => d.InstituteId);

        }
    }
}
