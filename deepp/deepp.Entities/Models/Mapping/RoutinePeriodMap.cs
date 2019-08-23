using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace deepp.Entities.Models.Mapping
{
    public class RoutinePeriodMap : EntityTypeConfiguration<RoutinePeriod>
    {
        public RoutinePeriodMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("RoutinePeriods");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.InstituteId).HasColumnName("InstituteId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.StartTime).HasColumnName("StartTime");
            this.Property(t => t.EndTime).HasColumnName("EndTime");
            this.Property(t => t.IsLeasure).HasColumnName("IsLeasure");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastUpdateTime).HasColumnName("LastUpdateTime");
            this.Property(t => t.AcademicBranchId).HasColumnName("AcademicBranchId");
            this.Property(t => t.AcademicShiftId).HasColumnName("AcademicShiftId");
            this.Property(t => t.OrderBy).HasColumnName("OrderBy");
            this.Property(t => t.RoutinePeriodTypeId).HasColumnName("RoutinePeriodTypeId");

            // Relationships
            this.HasOptional(t => t.AcademicBranch)
                .WithMany(t => t.RoutinePeriods)
                .HasForeignKey(d => d.AcademicBranchId);
            this.HasOptional(t => t.AcademicShift)
                .WithMany(t => t.RoutinePeriods)
                .HasForeignKey(d => d.AcademicShiftId);
            this.HasRequired(t => t.Institute)
                .WithMany(t => t.RoutinePeriods)
                .HasForeignKey(d => d.InstituteId);
            this.HasOptional(t => t.RoutinePeriodType)
                .WithMany(t => t.RoutinePeriods)
                .HasForeignKey(d => d.RoutinePeriodTypeId);

        }
    }
}
