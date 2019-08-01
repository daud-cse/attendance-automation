using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace pnsms.Entities.Models.Mapping
{
    public class AcademicShiftMap : EntityTypeConfiguration<AcademicShift>
    {
        public AcademicShiftMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("AcademicShifts");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.StartAt).HasColumnName("StartAt");
            this.Property(t => t.EndAt).HasColumnName("EndAt");
            this.Property(t => t.Remarks).HasColumnName("Remarks");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.TagId).HasColumnName("TagId");

            // Relationships
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.AcademicShifts)
                .HasForeignKey(d => d.InstituteId);
            this.HasOptional(t => t.Tag)
                .WithMany(t => t.AcademicShifts)
                .HasForeignKey(d => d.TagId);

        }
    }
}
